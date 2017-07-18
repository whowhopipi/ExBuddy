using Buddy.Coroutines;
using Clio.XmlEngine;
using ExBuddy.Data;
using ExBuddy.Helpers;
using ExBuddy.OrderBotTags.Behaviors.Objects;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [XmlElement("ExLevelSync")]
    public class ExLevelSync : ExProfileBehavior
    {

        protected async override Task<bool> Main()
        {
            StatusText = string.Format("等级同步");

            bool needLevelSync = false;

            // 判断是否需要等级同步
            // 1.判断是否在fate中
            if (FateManager.WithinFate && !Me.IsLevelSynced)
            {
                needLevelSync = true;
            }

            if(needLevelSync)
            {
                ToDoList.LevelSync();
            }

            isDone = true;

            return true;
        }
    }
}
