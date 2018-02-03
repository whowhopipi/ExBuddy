namespace ExBuddy.OrderBotTags.Craft.Order
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ff14bot.Objects;
    using ff14bot.Managers;
    using Buddy.Coroutines;
    using System.Threading;
    using ff14bot;
    using ff14bot.RemoteWindows;

    public class CustomOrder : BaseCraftOrder
    {
        public override string Name
        {
            get
            {
                return "Custom";
            }
        }

        internal List<string> Actions { set; get; }

        public int miniCp { get; set; }

        public override int MiniCp
        {
            get
            {
                return miniCp;
            }
        }
        
        public override async Task<bool> CheckSkills()
        {
            bool result = true;
            foreach(string action in Actions)
            {
                result = getAction(action) != null;

                if (!result)
                {
                    Logger.Warn("缺少技能{0}", action);
                    break;
                }
            }

            return result;
        }

        private SpellData getAction(string action)
        {
            if (IsGoodCondition)
            {
                if(string.Equals(action,"加工") && Core.Me.ClassLevel >= 53)
                {
                    action = "集中加工";
                } else if(string.Equals(action,"比尔格的祝福") && Core.Me.ClassLevel >= 51)
                {
                    action = "比尔格的技巧";
                }
            }
            return ActionManager.CurrentActions.Values.Find(ac => string.Equals(ac.Name, action, StringComparison.InvariantCultureIgnoreCase) || string.Equals(ac.LocalizedName, action, StringComparison.InvariantCultureIgnoreCase));
        }

        public override async Task<bool> Execute()
        {
            // 执行技能
            for (int i = 0; i < Actions.Count; i++)
            {
                string action = Actions[i];

                if (IsCrafting())
                {

                    SpellData spellData = getAction(action);

                    if(spellData == null)
                    {
                        Logger.Warn("缺少技能{0}", action);
                        break;
                    }


                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);

                    if (ActionManager.CanCast(action, null))
                    {
                        Logger.Info("Casting {0} ", spellData.LocalizedName);
                        ff14bot.Managers.ActionManager.DoAction(spellData, Core.Me);
                        await Coroutine.Wait(10000, () => CraftingManager.AnimationLocked);
                        await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked || SelectYesNoItem.IsOpen);
                        await Coroutine.Sleep(250);
                    }
                    else
                    {
                        Logger.Error("不能执行技能:{0}", action);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}