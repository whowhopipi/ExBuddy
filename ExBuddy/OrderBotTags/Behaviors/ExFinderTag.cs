using Buddy.Coroutines;
using Clio.XmlEngine;
using ExBuddy.Data;
using ExBuddy.Helpers;
using ExBuddy.OrderBotTags.Behaviors.Objects;
using ExBuddy.Windows;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.RemoteAgents;
using ff14bot.RemoteWindows;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [XmlElement("ExFinderTag")]
    public class ExFinderTag : ExProfileBehavior
    {

        [DefaultValue(-1)]
        [XmlAttribute("Category")]
        public int Category { get; set; }

        [DefaultValue(0)]
        [XmlAttribute("Index")]
        public int Index { set; get; }

        protected async override Task<bool> Main()
        {
            StatusText = "任务搜索器";
            
            while (!ContentsFinder.IsOpen)
            {
                ChatManager.SendChat("/dutyfinder");
                await Coroutine.Wait(3000, () => ContentsFinder.IsOpen);
            }

            await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => ContentsFinder.IsOpen);
            ExContentsFinder finder = new ExContentsFinder();
            
            /*
            if (Category > -1 && Index > 0)
            {
                Logger.Verbose("设置副本：第{0}类，第{1}个",Category,Index);
                finder.ChangePage((uint)Category);

                await Coroutine.Sleep(1000);

                finder.ChangeSelect((uint)Index);
                await Coroutine.Sleep(1000);
            }*/

            finder.Attend();

            if(await Coroutine.Wait(10000,() => ContentsFinderConfirm.IsOpen))
            {
                ContentsFinderConfirm.Commence();

                await Coroutine.Wait(10000, () => !ContentsFinderConfirm.IsOpen);

                await Coroutine.Wait(5000, () => CommonBehaviors.IsLoading);
                await CommonTasks.HandleLoading();
                await Coroutine.Yield();
            }
            isDone = true;

            return true;
        }
    }
}
