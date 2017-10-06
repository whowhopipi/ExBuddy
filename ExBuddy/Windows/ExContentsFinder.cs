namespace ExBuddy.Windows
{
    using Buddy.Coroutines;
    using ExBuddy.Enumerations;
    using ff14bot.Managers;
    using System.Threading.Tasks;

    // 大国联防军列表
    public sealed class ExContentsFinder : Window<ExContentsFinder>
	{
		public ExContentsFinder() : base("ContentsFinder") { }

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
            return TrySendAction(1,1,4);
        }

        public static async Task<bool> Open()
        {
            while (!IsOpen)
            {
                ChatManager.SendChat("/dutyfinder");
                await Coroutine.Wait(3000, () => IsOpen);
            }

            return true;
        }
    }
}
