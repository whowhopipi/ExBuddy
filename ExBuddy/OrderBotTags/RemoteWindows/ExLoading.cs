namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ExBuddy.OrderBotTags.Behaviors;
    using ff14bot.Behavior;
    using ff14bot.RemoteWindows;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    [LoggerName("ExLoading")]
    [XmlElement("ExLoading")]
    public class ExLoading : ExProfileBehavior
    {

        [DefaultValue(-1)]
        [XmlAttribute("WaitTime")]
        public int WaitTime { get; set; }

        protected override async Task<bool> Main()
        {
            Logger.Verbose("等待过场动画");
            StatusText = "等待加载动画";

            if(WaitTime == -1)
            {
                await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => CommonBehaviors.IsLoading);
            }
            else
            {
                await Coroutine.Wait(WaitTime, () => CommonBehaviors.IsLoading);
            }

            await CommonTasks.HandleLoading();

            isDone = true;

            return true;
        }
        
    }
}
