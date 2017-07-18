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
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    [XmlElement("GrandCompanySupply")]
    public class ExGrandCompanySupply : ExProfileBehavior
    {
        [DefaultValue(Locations.UldahStepsOfNald)]
        [XmlAttribute("Location")]
        public Locations Location { get; set; }

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
        
        private int Wait
        {
            get
            {
                return Convert.ToInt32(MathEx.Random(MaxWait, MinWait));
            }
        }

        protected override void OnStart()
        {
            var npcs = Data.GetNpcsByLocation(Location).ToArray();

            personnelOfficerNpc = npcs.OfType<GameObjects.Npcs.PersonnelOfficer>().FirstOrDefault();
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
                    name: Location + " NpcId: " + personnelOfficerNpc.NpcId);

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
                Log("暂不支持该地区：{0}", Location);
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

        protected override void OnDone()
        {
            if (GrandCompanySupplyList.IsOpen)
            {
                GrandCompanySupplyList.Close();
            }

            if (SelectYesno.IsOpen)
            {
                SelectYesno.ClickNo();
            }

            if (Request.IsOpen)
            {
                Request.Cancel();
            }
            
        }

        private async Task<bool> HandOver()
        {
            Log("开始交军票材料");
            if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
            {
                Log("NPC已找到，对话开始");
                SelectString.ClickSlot(0);

                if (await Coroutine.Wait(5000, () => GrandCompanySupplyList.IsOpen))
                {
                    while (true)
                    {
                        await Coroutine.Sleep(Wait);

                        var list = new GrandCompanySupplyList();
                        list.TurnIn(2);
                        await Coroutine.Yield();

                        SendActionResult result = list.HandOver(1);
                        if (result != SendActionResult.Success)
                        {
                            Logger.Info("递交物品返回值:{0},所有物品已提交完毕", result);
                            break;
                        }

                        if (await Coroutine.Wait(1000,() => SelectYesno.IsOpen || Request.IsOpen))
                        {
                            if (SelectYesno.IsOpen)
                            {
                                Log("提交优质物品");
                                SelectYesno.ClickYes();
                                await Coroutine.Wait(1000,()=>!SelectYesno.IsOpen);
                            } else if (Request.IsOpen)
                            {
                                Log("不是想要的提交物品，所有提交完成，结束");
                                Request.Cancel();
                                await Coroutine.Wait(1000,() => !Request.IsOpen);
                                break;
                            }
                        }

                        if (await Coroutine.Wait(5000, () => GrandCompanySupplyReward.IsOpen))
                        {
                            var reward = new GrandCompanySupplyReward();
                            reward.Yes();

                            if(await Coroutine.Wait(1000,() => SelectYesno.IsOpen))
                            {
                                Log("军票达到最大，不用再提交了");
                                SelectYesno.ClickNo();
                                await Coroutine.Wait(1000, () => !SelectYesno.IsOpen);
                                break;
                            }
                        }
                        else
                        {
                            Log("提交物品确认框未打开，可能有问题，结束");
                            break;
                        }
                    }

                    await Coroutine.Sleep(2000);

                    if (await Coroutine.Wait(5000, () => GrandCompanySupplyList.IsOpen))
                    {
                        GrandCompanySupplyList.Close();

                        await Coroutine.Sleep(2000);

                        await Coroutine.Wait(2000, () => !GrandCompanySupplyList.IsOpen);
                        await Coroutine.Wait(4000, () => SelectString.IsOpen);
                    }
                }
                else
                {
                    Log("列表未打开，可能有问题，结束");
                }

                await Coroutine.Yield();

                await Coroutine.Wait(2000, () => SelectString.IsOpen);

                if (SelectString.IsOpen)
                {
                    SelectString.ClickSlot(3);
                    await Coroutine.Wait(3000, () => !SelectString.IsOpen);
                    await Coroutine.Yield();
                }

            }
            else
            {
                Log("NPC未找到，结束");
            }

            isDone = true;

            return true;
        }

        protected override async Task<bool> Main()
        {
            if (!ScriptManager.GetCondition(Condition)()) {
                Log("当前不适合处理军票，条件：{0}",Condition);
                isDone = true;
                return true;
            }

            return HandleDeath() || await personnelOfficerNpc.TeleportTo() || await MoveToNpc()
                    || await InteractWithNpc() || await HandOver();
        }
        
    }
    
}
