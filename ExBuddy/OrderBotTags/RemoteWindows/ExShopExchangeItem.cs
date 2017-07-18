namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ExBuddy.Enumerations;
    using ExBuddy.Helpers;
    using ExBuddy.Windows;
    using ff14bot.Helpers;
    using ff14bot.Managers;
    using ff14bot.Objects;
    using ff14bot.RemoteWindows;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [LoggerName("ExShopExchangeItem")]
    [XmlElement("ExShopExchangeItem")]
    public class ExShopExchangeItem : ExRemoteWindowsBase
    {
        [XmlElement("ItemIds")]
        public List<int> ItemIds { set; get; }
        
        [XmlElement("HandNums")]
        public List<int> HandNums { set; get; }
        
        [XmlElement("Values")]
        public List<int> Values { set; get; }

        [XmlAttribute("Index")]
        public int Index { set; get; }

        [DefaultValue(0)]
        [XmlAttribute("Count")]
        public int Count { set; get; }

        [DefaultValue(1000)]
        [XmlAttribute("Delay")]
        public int Delay { set; get; }

        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        public string Condition { set; get; }

        private BagSlot[] items;
        private ShopExchangeItem shop;
        private Func<bool> condition;

        protected override void OnStart()
        {
            base.OnStart();

            condition = ScriptManager.GetCondition(Condition);
        }
        
        protected override async Task<bool> Main()
        {
            shop = new ShopExchangeItem();
            StatusText = "交换物品";

            if(ItemIds.Count != HandNums.Count && ItemIds.Count != Values.Count)
            {
                Logger.Error("递交物品和递交数量不匹配");
                isDone = true;
                return false;
            }

            if (await Coroutine.Wait(5000,() => ShopExchangeItem.IsOpen)) {

                int count = 1;
                while(condition())
                {
                    StatusText = "第" + count + "次交换";
                    if(Count == 0 || count <= Count)
                    {
                        if(await doOneExchange())
                        {
                            count++;
                        } else
                        {
                            Logger.Error("交换失败");
                            break;
                        }
                    } else
                    {
                        break;
                    }

                    await Coroutine.Sleep(Delay);
                }
            } else
            {
                Logger.Info("物品交换窗口没打开");
            }

            ShopExchangeItem.Close();

            await Coroutine.Wait(6000, ()=> !ShopExchangeItem.IsOpen);

            isDone = true;

            return true;
        }
        
        private async Task<bool> doOneExchange()
        {
            items = new BagSlot[ItemIds.Count];

            // 查询物品是否存在
            for (int num=0;num<ItemIds.Count;num++)
            {
                int itemId = ItemIds[num];
                int value = Values[num];
                int handNum = HandNums[num];
                BagSlot item = null;
                var slots = InventoryManager.FilledSlots.Where(i => !Blacklist.Contains((uint)i.Pointer.ToInt32(), BlacklistFlags.Loot)).ToArray();

                if (value > 0)
                {
                    item = slots.FirstOrDefault(i => i.RawItemId == itemId && i.Collectability >= value);
                }
                else
                {
                    item = slots.FirstOrDefault(i => i.RawItemId == itemId && i.Collectability == 0 && i.Count >= handNum);
                }

                if (item == null)
                {
                    Logger.Warn("未找到合适的物品，递交结束");
                    return false;
                }

                items[num] = item;
            }

            SendActionResult result = shop.PurchaseItem((uint)Index);

            // 等待确认对话框出现
            await Coroutine.Wait(3000,()=> ShopExchangeItemDialog.IsOpen);
            new ShopExchangeItemDialog().Yes();

            await Coroutine.Wait(2000, () => !ShopExchangeItemDialog.IsOpen);

            // 等待递交窗口打开
            if (await Coroutine.Wait(3000,() => Request.IsOpen))
            {
                foreach(BagSlot item in items)
                {
                    Logger.Verbose("递交物品{0}({1})", item.Name, item.RawItemId);
                    item.Handover();
                }
                await Coroutine.Wait(2000, () => Request.HandOverButtonClickable);
                Request.HandOver();

                await Coroutine.Wait(4000, () => !Request.IsOpen);
            } else
            {
                // 递交窗口未打开，说明不能递交，失败
                Logger.Error("递交窗口未打开，交换失败，重试一次");
            }

            return true;
        }
    }
}
