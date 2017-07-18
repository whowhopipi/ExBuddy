using Buddy.Coroutines;
using Clio.Utilities;
using Clio.XmlEngine;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [XmlElement("ExWaitWhile")]
    public class ExWaitWhile : ExProfileBehavior
    {
        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        public String Condition { get; set; }

        private Func<bool> condition;

        protected override void OnStart()
        {
            condition = ScriptManager.GetCondition(Condition);
        }

        protected override async Task<bool> Main()
        {
            StatusText = "条件等待：" + Condition;

            if (condition())
            {
                await Coroutine.Sleep(3000);
            } else
            {
                isDone = true;
            }
            return true;
        }

    }
}
