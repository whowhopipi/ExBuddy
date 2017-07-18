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

    [LoggerName("ExTalk")]
    [XmlElement("ExTalk")]
    public class ExTalk : ExProfileBehavior
    {

        [DefaultValue(-1)]
        [XmlAttribute("WaitTime")]
        public int WaitTime { get; set; }

        protected override async Task<bool> Main()
        {
            Logger.Verbose("对话");

            StatusText = "对话";

            if(WaitTime == -1)
            {
                await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => Talk.DialogOpen);
            }
            else
            {
                await Coroutine.Wait(WaitTime, () => Talk.DialogOpen);
            }

            do
            {
                Talk.Next();
                await Coroutine.Yield();
            } while (Talk.DialogOpen);

            isDone = true;

            return true;
        }
        
    }
}
