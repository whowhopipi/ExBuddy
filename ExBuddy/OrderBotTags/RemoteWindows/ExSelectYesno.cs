namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ExBuddy.OrderBotTags.Behaviors;
    using ff14bot.RemoteWindows;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    [LoggerName("ExSelectYesno")]
    [XmlElement("ExSelectYesno")]
    public class ExSelectYesno : ExRemoteWindowsBase
    {
        
        [DefaultValue(true)]
        [XmlAttribute("Yes")]
        public bool Yes { set; get; }

        protected override async Task<bool> Main()
        {
            if(WaitTime == -1)
            {
                await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => SelectYesno.IsOpen);
            }
            else
            {
                await Coroutine.Wait(WaitTime, () => SelectYesno.IsOpen);
            }
            
            if (SelectYesno.IsOpen)
            {
                if (Yes)
                {
                    SelectYesno.ClickYes();
                } else
                {
                    SelectYesno.ClickNo();
                }

                await Coroutine.Wait(1000, () => !SelectYesno.IsOpen);
            } else
            {
                return false;
            }
            await Coroutine.Yield();

            isDone = true;

            return true;
        }
        
    }
}
