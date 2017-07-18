namespace ExBuddy.OrderBotTags.Behaviors
{
    using System;
    using System.Threading.Tasks;
    using Buddy.Coroutines;
    using ff14bot.Managers;
    using Clio.XmlEngine;
    using ff14bot.RemoteWindows;
    using System.Text.RegularExpressions;
    using ff14bot.RemoteAgents;
    using System.Threading;
    using System.ComponentModel;
    using ff14bot.Objects;
    using ff14bot;

    [XmlElement("Chocobo")]
    [XmlElement("ExChocobo")]
    public class ExChocobo : ExProfileBehavior
    {
        [DefaultValue(8165)]
        [XmlAttribute("FoodId")]
        public int FoodId { get; set; }

        [XmlAttribute("PlayerName")]
        public String PlayerName { get; set; }

        [DefaultValue(6.0f)]
        [XmlAttribute("Radius")]
        public float Radius { get; set; }

        private HousingEventObject chocobo;
        
        protected override void OnStart()
        {
            foreach(HousingEventObject one in GameObjectManager.GetObjectsOfType<HousingEventObject>()) {
                if (one.Distance(Core.Me.Location) < Radius)
                {
                    chocobo = one;
                    break;
                }
            }
            
        }

        protected override async Task<bool> Main()
        {
            if (chocobo == null)
            {
                Log("未找到陆行鸟棚，或者陆行鸟棚距离过远");
                isDone = true;
                return true;
            }
            
            chocobo.Interact();
            await Coroutine.Sleep(2000);

            if (SelectString.IsOpen)
            {
                SelectString.ClickSlot(0);
                await Coroutine.Wait(5000, () => HousingChocoboList.IsOpen || HousingMyChocobo.IsOpen);
                //Give it sometime to populate data
                await Coroutine.Sleep(3000);
            }

            //Make sure we didn't just timeout above
            if (HousingChocoboList.IsOpen)
            {
                await HousingChocoboListWork();
            }
            else if (HousingMyChocobo.IsOpen)
            {
                await HousingMyChocoboWork();
            }
            else
            {
                Log("陆行鸟棚没有打开，结束");
            }

            if (HousingChocoboList.IsOpen)
            {
                HousingChocoboList.Close();
                await Coroutine.Wait(2000, () => !HousingChocoboList.IsOpen);
                await Coroutine.Wait(2000, () => SelectString.IsOpen);
            }
            else if (HousingMyChocobo.IsOpen)
            {
                HousingMyChocobo.Close();
                await Coroutine.Wait(2000, () => !HousingMyChocobo.IsOpen);
                await Coroutine.Wait(2000, () => SelectString.IsOpen);
            }
            
            if (SelectString.IsOpen)
            {
                SelectString.ClickSlot(4);
                await Coroutine.Wait(2000, () => !SelectString.IsOpen);
            }

            return isDone = true;
        }

        internal static Regex TimeRegex = new Regex(@"(?:.*?)(\d+).*", RegexOptions.Compiled);
        private async Task HousingMyChocoboWork()
        {
            var matches = TimeRegex.Match(HousingMyChocobo.Lines[0]);
            if (!matches.Success)
            {
                //We are ready to train now
                HousingMyChocobo.SelectLine(0);

                Log("Waiting for inventory menu to appear....");
                //Wait for the inventory window to open and be ready
                //Technically the inventory windows are always 'open' so we check if their callbackhandler has been set
                if (!await Coroutine.Wait(5000, () => AgentInventoryContext.Instance.CallbackHandlerSet))
                {
                    Log("Inventorymenu failed to appear, aborting current iteration and starting over.");
                    return;
                }

                Log("Feeding Chocobo {0}", FoodId);
                AgentHousingBuddyList.Instance.Feed((uint)FoodId);

                Log("Waiting for cutscene to start....");
                if (await Coroutine.Wait(5000, () => QuestLogManager.InCutscene))
                {
                    Log("Waiting for cutscene to end....");
                    await Coroutine.Wait(Timeout.Infinite, () => !QuestLogManager.InCutscene);
                }

                Log("Waiting for menu to reappear....");
                await Coroutine.Wait(Timeout.Infinite, () => HousingMyChocobo.IsOpen);
                await Coroutine.Sleep(1000);
            }
            else
            {
                var timeToSleep = TimeSpan.FromMinutes(Int32.Parse(matches.Groups[1].Value) + 1);
                HousingMyChocobo.Close();
                Log(@"Sleeping for {0},until our chocobo is ready.", timeToSleep);
                await Coroutine.Sleep(timeToSleep);
            }
        }

        private async Task HousingChocoboListWork()
        {
            //Look for our chocobo
            var items = HousingChocoboList.Items;
            var targetName = PlayerName;

            //512 possible chocobos, 14 items per page...
            for (uint stableSection = 0; stableSection < AgentHousingBuddyList.Instance.TotalPages; stableSection++)
            {

                if (stableSection != AgentHousingBuddyList.Instance.CurrentPage)
                {
                    Log("切换到第{0}页", stableSection);
                    HousingChocoboList.SelectSection(stableSection);
                    await Coroutine.Sleep(5000);
                    items = HousingChocoboList.Items;
                }

                for (uint i = 0; i < items.Length; i++)
                {
                    var currentChocobo = items[i];

                    if (string.IsNullOrEmpty(currentChocobo.PlayerName))
                        continue;

                    if (string.Equals(currentChocobo.PlayerName, targetName, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(targetName))
                    {
                        if (currentChocobo.ReadyAt < DateTime.Now)
                        {
                            await Coroutine.Sleep(1000);
                            //Personal chocobo is handled differently

                            bool flag = false;
                            for(int tickit = 0;tickit < 5; tickit++)
                            {
                                if (i == 0)
                                {
                                    Logger.Info("选择自己的陆行鸟");
                                    HousingChocoboList.SelectMyChocobo();
                                }
                                else
                                {
                                    Log("在第{0}页选择第{1}个陆行鸟：{2}/{3}", stableSection, i, currentChocobo.ChocoboName, currentChocobo.PlayerName);
                                    HousingChocoboList.SelectChocobo(i);
                                }
                                flag = await Coroutine.Wait(2000, () => SelectYesno.IsOpen || AgentInventoryContext.Instance.CallbackHandlerSet);

                                if (flag)
                                {
                                    break;
                                } else
                                {
                                    Logger.Info("超时，重试，第{0}次",tickit);
                                }
                            }

                            if (!flag)
                            {
                                Logger.Info("物品栏没有打开.");
                                continue;
                            }

                            //检查陆行鸟是否满级了
                            if (SelectYesno.IsOpen)
                            {
                                Logger.Info("陆行鸟({0}/{1})已经满级", currentChocobo.ChocoboName, currentChocobo.PlayerName);
                                SelectYesno.ClickNo();
                                continue;
                            }

                            Log("Feeding Chocobo {0}", FoodId);
                            AgentHousingBuddyList.Instance.Feed((uint)FoodId);

                            await Coroutine.Yield();

                            if (SelectYesno.IsOpen)
                            {
                                SelectYesno.ClickYes();
                                await Coroutine.Wait(2000, () => !SelectYesno.IsOpen);
                            }

                            Log("Waiting for cutscene to start....");
                            if (await Coroutine.Wait(5000, () => QuestLogManager.InCutscene))
                            {
                                Log("Waiting for cutscene to end....");
                                await Coroutine.Wait(Timeout.Infinite, () => !QuestLogManager.InCutscene);
                            }

                            Log("Waiting for menu to reappear....");
                            await Coroutine.Wait(Timeout.Infinite, () => HousingChocoboList.IsOpen);
                            
                        }
                    }
                }
            }
        }
    }
}
