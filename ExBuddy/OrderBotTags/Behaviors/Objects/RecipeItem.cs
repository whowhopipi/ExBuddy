namespace ExBuddy.OrderBotTags.Behaviors.Objects
{
    using ff14bot.Enums;
    public class RecipeItem
    {
        public int Id { set; get; }

        public int Level { set; get; }

        public string ItemName { set; get; }

        public ClassJobType ClassJob { set; get; }

        public bool CanHq { set; get; }

        public int Difficulty { set; get; }

        public int Durability { set; get; }

        public int Quality { set; get; }
    }
}