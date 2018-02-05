namespace ExBuddy.OrderBotTags.Craft.Order
{
    using Behaviors.Objects;
    using Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Level703Star70DurabilityNonSpecialistCraft : BaseCraftOrder
    {
        public override string Name
        {
            get
            {
                return "Level703Star70DurabilityNonSpecialistCraft";
            }
        }

        public override int MiniCp
        {
            get
            {
                return 538;
            }
        }
		
        public bool CanExecute(RecipeItem recipe)
        {
            return recipe.Level == 70;
        }
        
        public override List<CraftActions> NeedSkills()
        {
            List<CraftActions> needActions = new List<CraftActions>();
            needActions.Add(CraftActions.MuscleMemory);
            needActions.Add(CraftActions.ComfortZone);
            needActions.Add(CraftActions.InnerQuiet);
            needActions.Add(CraftActions.ManipulationII);
            needActions.Add(CraftActions.SteadyHandII);
            needActions.Add(CraftActions.PiecebyPiece);
            needActions.Add(CraftActions.PrudentTouch);
            needActions.Add(CraftActions.Observe);
            needActions.Add(CraftActions.FocusedTouch);
            needActions.Add(CraftActions.SteadyHand);
            needActions.Add(CraftActions.BasicTouch);
            needActions.Add(CraftActions.GreatStrides);
            needActions.Add(CraftActions.Innovation);
            needActions.Add(CraftActions.ByregotsBlessing);
            needActions.Add(CraftActions.CarefulSynthesisII);
            return needActions;
        }
        
        public override async Task<bool> DoExecute()
        {
            await Cast(CraftActions.MuscleMemory);
            await Cast(CraftActions.ComfortZone);
            await Cast(CraftActions.InnerQuiet);
            await Cast(CraftActions.ManipulationII);
            await Cast(CraftActions.SteadyHandII);
            await Cast(CraftActions.PiecebyPiece);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.SteadyHandII);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.PrudentTouch);
            await Cast(CraftActions.ComfortZone);
            await Cast(CraftActions.Observe);
            await Cast(CraftActions.FocusedTouch);

            await Cast(CraftActions.SteadyHand);
            await Cast(CraftActions.PiecebyPiece);
            await Cast(CraftActions.Innovation);
            await Cast(CraftActions.GreatStrides);

            if(ConditionExcellent)
            {
                await Cast(CraftActions.ByregotsBlessing);
                await Cast(CraftActions.IngenuityII);
            } else
            {
                await Cast(CraftActions.IngenuityII);
                await Cast(CraftActions.ByregotsBlessing);
            }
            
            await Cast(CraftActions.Observe);
            await Cast(CraftActions.FocusedSynthesis);
            await Cast(CraftActions.Observe);
            await Cast(CraftActions.FocusedSynthesis);

            return true;
        }
        

    }
}