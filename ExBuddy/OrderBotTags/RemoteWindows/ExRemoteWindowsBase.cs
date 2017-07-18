namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ExBuddy.OrderBotTags.Behaviors;
    using System.ComponentModel;
    using System.Threading.Tasks;

    [LoggerName("ExInteract")]
    public abstract class ExRemoteWindowsBase : ExProfileBehavior
    {

        [DefaultValue(-1)]
        [XmlAttribute("WaitTime")]
        public int WaitTime { get; set; }

        [DefaultValue(0)]
        [XmlAttribute("RetryTime")]
        public int RetryTime { set; get; }
        
        private int retryCount = 0;
        
        protected override void OnStart()
        {
            retryCount = 0;
        }

        protected override async Task<bool> DoMainFailed()
        {
            retryCount++;

            if(retryCount >= RetryTime)
            {
                isDone = true;
            }

            await Coroutine.Sleep(WaitTime);

            return true;
        }
        
    }
}
