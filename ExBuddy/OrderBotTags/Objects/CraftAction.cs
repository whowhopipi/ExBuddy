namespace ExBuddy.OrderBotTags.Objects
{
    using ff14bot.Enums;
    public class CraftAction
    {
        public int Id { set; get; }

        public string Code { set; get; }
        
        public uint Carpenter { set; get; }

        public uint Blacksmith { set; get; }

        public uint Armorer { set; get; }

        public uint Goldsmith { set; get; }

        public uint Leatherworker { set; get; }

        public uint Weaver { set; get; }

        public uint Alchemist { set; get; }

        public uint Culinarian { set; get; }
    }
}