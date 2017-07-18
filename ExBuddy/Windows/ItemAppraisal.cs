namespace ExBuddy.Windows
{
	using ExBuddy.Enumerations;

	public sealed class ItemAppraisal : Window<ItemAppraisal>
	{
		public ItemAppraisal()
			: base("ItemAppraisal") {}

		public SendActionResult Yes()
		{
			return TrySendAction(1, 3, 0);
		}
	}
}