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
    using ExBuddy.Helpers;

    public class CustomOrder : BaseCraftOrder
    {
        public override string Name
        {
            get
            {
                return "Custom";
            }
        }

        internal List<CraftActions> Actions { set; get; }

        public int miniCp { get; set; }

        public override int MiniCp
        {
            get
            {
                return miniCp;
            }
        }

        public override List<CraftActions> NeedSkills()
        {
            List<CraftActions> needActions = new List<CraftActions>();

            foreach (var action in Actions) { 
                if (!needActions.Contains(action))
                {
                    needActions.Add(action);
                }
            }
            return needActions;
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
                CraftActions action = Actions[i];

                if (IsCrafting)
                {
                    bool flag = await Cast(action);
                    if (!flag) return false;
                }
            }
            return true;
        }
    }
}