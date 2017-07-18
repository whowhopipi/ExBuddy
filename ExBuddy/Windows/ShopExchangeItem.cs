namespace ExBuddy.Windows
{
    using ExBuddy.Enumerations;

    public sealed class ShopExchangeItem : Window<ShopExchangeItem>
	{
		public ShopExchangeItem()
			: base("ShopExchangeItem") {}

		public SendActionResult PurchaseItem(uint index)
		{
			return TrySendAction(2, 0, 0, 1, index);
		}
        
	}
}