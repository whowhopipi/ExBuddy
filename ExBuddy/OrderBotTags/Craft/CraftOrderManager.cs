namespace ExBuddy.OrderBotTags.Craft
{
    using Order;
    using Objects;
    using System;
    using System.Collections.Generic;

    public class CraftOrderManager
    {
        private static readonly List<BaseCraftOrder> CraftOrderList = new List<BaseCraftOrder>()
        {
            new Level70FlawlessCraft()
        };

        public static BaseCraftOrder GetOrder(RecipeItem recipe)
        {
            foreach(BaseCraftOrder order in CraftOrderList)
            {
                if (order.CanExecute(recipe))
                {
                    return order;
                }
            }
            return null;
        }

        internal static BaseCraftOrder NewCustomOrder(List<string> Actions,int MiniCp)
        {
            CustomOrder order = new CustomOrder() {
                miniCp = MiniCp,
                Actions = Actions
            };
            return order;
        }

        public static BaseCraftOrder GetOrderByName(string OrderName)
        {
            foreach(BaseCraftOrder order in CraftOrderList)
            {
                if (string.Equals(OrderName, order.Name, StringComparison.InvariantCulture))
                {
                    return order;
                }
            }
            return null;
        }
    }
}
