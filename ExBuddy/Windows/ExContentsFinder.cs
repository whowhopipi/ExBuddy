namespace ExBuddy.Windows
{
    using ExBuddy.Enumerations;

    // 大国联防军列表
    public sealed class ExContentsFinder : Window<ExContentsFinder>
	{
		public ExContentsFinder()
			: base("ContentsFinder") { }

        public SendActionResult ChangePage(uint index)
        {
            return TrySendAction(2, 1, 2, 1, index);
        }

        public SendActionResult ChangeSelect(uint index)
        {
            return TrySendAction(2, 1, 4, 1, index + 1);
        }

        public SendActionResult Attend()
        {
            return TrySendAction(2, 1, 5, 1, 1);
        }

    }
}
