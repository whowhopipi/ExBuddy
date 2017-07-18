namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ff14bot.RemoteWindows;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    [LoggerName("ExSelect")]
    [XmlElement("ExSelect")]
    [XmlElement("ExSelectIconString")]
    [XmlElement("ExSelectString")]
    public class ExSelect : ExRemoteWindowsBase
    {
        
        [DefaultValue(-1)]
        [XmlAttribute("Slot")]
        public int Slot { set; get; }

        [XmlAttribute("ContainText")]
        public string ContainText { set; get; }

        [XmlAttribute("FullText")]
        public string FullText { set; get; }

        protected override async Task<bool> Main()
        {
            StatusText = "选择框";

            if(WaitTime == -1)
            {
                await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => SelectString.IsOpen || SelectIconString.IsOpen);
            }
            else
            {
                await Coroutine.Wait(WaitTime, () => SelectString.IsOpen || SelectIconString.IsOpen);
            }

            if (SelectString.IsOpen)
            {
                Logger.Verbose("SelectString打开了");
                if (Slot >= 0)
                {
                    Logger.Verbose("选择第{0}行",Slot);
                    SelectString.ClickSlot((uint)Slot);
                }
                else if (FullText != null && !FullText.Equals(""))
                {
                    Logger.Verbose("选择文本为({0})的行", FullText);
                    SelectString.ClickLineEquals(FullText);
                }
                else if (ContainText != null && !ContainText.Equals(""))
                {
                    Logger.Verbose("选择包含文本({0})的行", ContainText);
                    SelectString.ClickLineContains(ContainText);
                }

                await Coroutine.Wait(Timeout.Infinite, () => !SelectString.IsOpen);
            }
            else if (SelectIconString.IsOpen)
            {
                Logger.Verbose("SelectIconString打开了");
                if (Slot >= 0)
                {
                    Logger.Verbose("选择第{0}行", Slot);
                    SelectIconString.ClickSlot((uint)Slot);
                }
                else if (FullText != null && !FullText.Equals(""))
                {
                    Logger.Verbose("选择文本为({0})的行", FullText);
                    SelectIconString.ClickLineEquals(FullText);
                }
                else if (ContainText != null && !ContainText.Equals(""))
                {
                    Logger.Verbose("选择包含文本({0})的行", ContainText);
                    SelectIconString.ClickLineContains(ContainText);
                }

                await Coroutine.Wait(Timeout.Infinite, () => !SelectIconString.IsOpen);
            }
            else
            {
                Logger.Verbose("没有选择框打开");
                return false;
            }
            await Coroutine.Yield();

            isDone = true;

            return true;
        }
        
    }
}
