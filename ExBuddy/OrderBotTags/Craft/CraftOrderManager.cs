﻿namespace ExBuddy.OrderBotTags.Craft
{
    using Order;
    using Objects;
    using System;
    using System.Collections.Generic;
    using ExBuddy.Helpers;

    public class CraftOrderManager
    {
        private static readonly List<BaseCraftOrder> CraftOrderList = new List<BaseCraftOrder>()
        {
            new Level70FlawlessCraft(),
            new Level703Star35DurabilityNonSpecialistCraft(),
            new Level703Star70DurabilityNonSpecialistCraft()
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

        internal static BaseCraftOrder NewCustomOrder(List<CraftActions> Actions,int MiniCp)
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
