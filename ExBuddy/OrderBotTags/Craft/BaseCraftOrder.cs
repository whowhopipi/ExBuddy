namespace ExBuddy.OrderBotTags.Craft
{
    using Buddy.Coroutines;
    using ExBuddy.Helpers;
    using ExBuddy.Logging;
    using ff14bot;
    using ff14bot.Enums;
    using ff14bot.Managers;
    using ff14bot.Objects;
    using ff14bot.RemoteWindows;
    using Interfaces;
    using Objects;
    using System.Threading;
    using System.Threading.Tasks;
    public abstract class BaseCraftOrder : ICraftOrder
    {
        protected bool hasSkills = true;

        public abstract int MiniCp { get; }

        public abstract string Name { get; }

        public Logger Logger{ get; set;}

        public RecipeItem recipe { get; set; }

        public string param { get; set; }
        
        protected bool IsCrafting()
        {
            return CraftingManager.IsCrafting && CraftingManager.ProgressRequired != CraftingManager.Progress;
        }
        
        internal async Task<bool> checkSkill(Ability action,string errmsg)
        {
            bool result = false;

            int ticket = 0;

            while(ticket < 5)
            {
                result = HasAction(action);
                if (result)
                {
                    break;
                } else
                {
                    await Coroutine.Sleep(5000);
                }
                ticket++;
            }

            if (!result)
            {
                Logger.Error("{0}", errmsg);
                return false;
            }

            return true;
        }
        
        protected bool IsGoodCondition()
        {
            return CraftingManager.Condition == CraftingCondition.Good || CraftingManager.Condition == CraftingCondition.Excellent;
        }

        internal async Task<bool> Cast(Ability id)
        {
            bool result = false;
            uint Action = Abilities.Map[Core.Me.CurrentJob][id];

            SpellData spellData = DataManager.SpellCache[Action];

            await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);
            
            if (ff14bot.Managers.ActionManager.CanCast(Action, null))
            {
                Logger.Info("Casting {0} ({1})", spellData.LocalizedName, id);
                ff14bot.Managers.ActionManager.DoAction(spellData, Core.Me);
                await Coroutine.Wait(10000, () => CraftingManager.AnimationLocked);
                await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked || SelectYesNoItem.IsOpen);
                await Coroutine.Sleep(250);

                result = true;
            }
            return result;
        }

        internal bool HasAction(Ability id)
        {
            return ff14bot.Managers.ActionManager.CurrentActions.ContainsKey(Abilities.Map[Core.Player.CurrentJob][id]);
        }

        public virtual bool CanExecute(RecipeItem recipe)
        {
            return true;
        }

        public virtual Task<bool> Execute()
        {
            OnStart();
            return DoExecute();
        }

        public virtual async Task<bool> DoExecute() { return true; }

        public abstract Task<bool> CheckSkills();

        public virtual async Task<bool> OnStart() { return true; }

        public bool CheckCp()
        {
            bool flag = Core.Me.MaxCP >= MiniCp;
            if (!flag)
            {
                Logger.Error("制作力最低要求{0}", MiniCp);
            }
            return flag;
        }

        internal bool HasAura(AbilityAura auraId)
        {
            return Core.Me.HasAura((uint)auraId);
        }

        internal bool CanCast(Ability id)
        {
            return Actions.CanCast(id);
        }


    }
}