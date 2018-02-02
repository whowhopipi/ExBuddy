namespace ExBuddy.OrderBotTags.Behaviors.Objects
{
    public enum ShopType
    {
#if RB_CN
		RedCrafter50,

		RedCrafter61,

		YellowCrafter,

		YellowCrafterItems,

		RedGatherer50,

		RedGatherer61,

		YellowGatherer,

		YellowGathererItems
#else
        RedCrafter50,

        RedCrafter61,

        RedCrafterMasterRecipes,

        YellowCrafter,

        YellowCrafterAugmentation,

        YellowCrafterItems,

        RedGatherer50,

        RedGatherer61,

        YellowGatherer,

        YellowGathererAugmentation,

        YellowGathererItems
#endif
    }
}