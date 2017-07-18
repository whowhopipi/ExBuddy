namespace ExBuddy.Windows
{
	using System.Threading.Tasks;
	using Buddy.Coroutines;
	using ExBuddy.Enumerations;
	using ExBuddy.Helpers;
	using ff14bot.RemoteWindows;

	public sealed class ShopExchangeItemDialog : Window<ShopExchangeItemDialog>
	{
		public ShopExchangeItemDialog()
			: base("ShopExchangeItemDialog") {}

        public SendActionResult Yes()
        {
            return TrySendAction(1, 3, 0);
        }

    }
}