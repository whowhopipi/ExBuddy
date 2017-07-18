namespace ExBuddy.OrderBotTags.Behaviors
{
    using Buddy.Coroutines;
    using Clio.Common;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using Enumerations;
    using ExBuddy.Helpers;
    using ExBuddy.Interfaces;
    using ExBuddy.OrderBotTags.Behaviors.Objects;
    using ExBuddy.Windows;
    using ff14bot.Helpers;
    using ff14bot.Managers;
    using ff14bot.RemoteWindows;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    [XmlElement("ExShopExchange")]
    public class ExShopExchange : ExProfileBehavior
    {
        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        public string Condition { get; set; }

        [DefaultValue(500)]
        [XmlAttribute("MaxWait")]
        public int MaxWait { set; get; }

        [DefaultValue(250)]
        [XmlAttribute("MinWait")]
        public int MinWait { set; get; }
        
        private INpc personnelOfficerNpc;
        private Func<bool> condition;
        
        private int Wait
        {
            get
            {
                return Convert.ToInt32(MathEx.Random(MaxWait, MinWait));
            }
        }

        protected override void OnStart()
        {
            var npcs = Data.GetNpcsByLocation(Locations.MorDhona).ToArray();

            personnelOfficerNpc = npcs.OfType<GameObjects.Npcs.ShopExchangeItem>().FirstOrDefault();

            condition = ScriptManager.GetCondition(Condition);
        }

        protected async Task<bool> MoveToNpc()
        {
            if (Me.Location.Distance(personnelOfficerNpc.Location) <= 4)
            {
                // we are already there, continue
                return false;
            }

            StatusText = "Moving to Npc -> " + personnelOfficerNpc.NpcId;

            await
                personnelOfficerNpc.Location.MoveTo(radius: 3.9f,
                    name: Locations.MorDhona + " NpcId: " + personnelOfficerNpc.NpcId);

            return false;
        }

        private bool HandleDeath()
        {
            if (Me.IsDead && Poi.Current.Type != PoiType.Death)
            {
                Poi.Current = new Poi(Me, PoiType.Death);
                return true;
            }

            return false;
        }

        private async Task<bool> InteractWithNpc()
        {
            if(personnelOfficerNpc == null)
            {
                Log("暂不支持该地区：{0}", Locations.MorDhona);
                isDone = true;
                return true;
            }

            if (Me.Location.Distance(personnelOfficerNpc.Location) > 4)
            {
                // too far away, should go back to MoveToNpc
                return true;
            }

            if (GameObjectManager.Target != null && MasterPieceSupply.IsOpen)
            {
                // already met conditions
                return false;
            }

            await personnelOfficerNpc.Interact(4);

            StatusText = "Interacting with Npc -> " + personnelOfficerNpc.NpcId;
            await Coroutine.Yield();

            return false;
        }
        
        private async Task<bool> HandOver()
        {
            Log("开始兑换工匠下框眼镜");
            if (await Coroutine.Wait(5000, () => SelectIconString.IsOpen))
            {
                Log("NPC已找到，对话开始");
                SelectIconString.ClickSlot(2);

                Logger.Verbose("等待对话框打开");
                await Coroutine.Wait(5000, () => Talk.DialogOpen);

                Logger.Verbose("进入下一个对话");
                Talk.Next();

                Logger.Verbose("等待兑换物品窗口打开");
                await Coroutine.Wait(5000, () => ShopExchangeItem.IsOpen);

                ShopExchangeItem itemWindow = new ShopExchangeItem();
                ShopExchangeItemDialog dialog = new ShopExchangeItemDialog();

                uint idx = 0;
                Logger.Verbose("准备开始循环");
                while (condition() && idx < 8)
                {
                    await Coroutine.Sleep(Wait);

                    await Coroutine.Wait(5000, () => ShopExchangeItem.IsOpen && !ShopExchangeItemDialog.IsOpen && !Request.IsOpen);

                    Logger.Verbose("尝试提交第{0}行物品", idx);
                    itemWindow.PurchaseItem(idx);

                    Logger.Verbose("等待物品提交确认窗口打开");
                    await Coroutine.Wait(5000, () => ShopExchangeItemDialog.IsOpen);

                    Logger.Verbose("选择交换");
                    dialog.Yes();

                    Logger.Verbose("等待提交物品请求框打开");
                    if(await Coroutine.Wait(2000, () => Request.IsOpen || !ShopExchangeItemDialog.IsOpen))
                    {
                        if (Request.IsOpen)
                        {
                            Logger.Verbose("提交物品请求框已打开");
                            Request.HandOver();
                            var itemCount = Memory.Request.ItemsToTurnIn.Length;
                            var itemId = Memory.Request.ItemId1;

                            IEnumerable<BagSlot> itemSlots =
                                InventoryManager.FilledInventoryAndArmory.Where(
                                    bs => bs.RawItemId == itemId && !Blacklist.Contains((uint)bs.Pointer.ToInt32(), BlacklistFlags.Loot)).ToArray();

                            itemSlots = itemSlots.Where(bs => bs.IsHighQuality);

                            var items = itemSlots.Take(itemCount).ToArray();

                            if (items.Length == 0)
                            {
                                Logger.Info("一个物品栏物品数量不足，换下一行");
                                idx++;
                                continue;
                            }

                            foreach (var item in items)
                            {
                                item.Handover();
                                await Coroutine.Yield();
                            }

                            if (await Coroutine.Wait(1000, () => Request.HandOverButtonClickable))
                            {
                                Logger.Verbose("已提交足够物品，等待下一步");
                                Request.HandOver();
                            }
                            else
                            {
                                Logger.Info("物品提交失败，换下一行");
                                idx++;
                                Request.Cancel();
                                await Coroutine.Yield();
                            }
                        } else if (!ShopExchangeItemDialog.IsOpen)
                        {
                            Logger.Verbose("提交物品确认框消失了，提交物品不足，换下一行");
                            idx++;
                            continue;
                        }
                    }
                    else
                    {
                        Logger.Info("提交物品框未打开，可能提交失败，换下一个");
                        idx++;
                    }
                }
            }
            else
            {
                Log("NPC对话未打开，结束");
            }

            isDone = true;

            return true;
        }

        protected override async Task<bool> Main()
        {
            if (!condition()) {
                Log("当前不适合处理军票，条件：{0}",Condition);
                isDone = true;
                return true;
            }

            return HandleDeath() || await personnelOfficerNpc.TeleportTo() || await MoveToNpc()
                    || await InteractWithNpc() || await HandOver();
        }
        
    }
    
}
