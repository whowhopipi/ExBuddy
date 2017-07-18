namespace ExBuddy.OrderBotTags.Behaviors
{
    using Buddy.Coroutines;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using ff14bot.Managers;
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    [XmlElement("ExHalfTimeWait")]

    public class ExHalfTimeWait : ExProfileBehavior
    {
        public enum HalfType
        {
            Up,

            Down
        }

        [DefaultValue(HalfType.Down)]
        [XmlAttribute("Type")]
        public HalfType Type { get; set; }

        

        protected override void OnStart()
        {
            // 获取游戏时间
        }

        protected override async Task<bool> Main()
        {
            int minute = WorldManager.EorzaTime.Minute;

            switch (Type)
            {
                case HalfType.Up:
                    if(minute < 30)
                    {
                        await Coroutine.Sleep(1000 * (30 - minute) * 3);
                    } else
                    {
                        await Coroutine.Sleep(3000);
                    }
                    break;
                case HalfType.Down:
                    if(minute > 30)
                    {
                        await Coroutine.Sleep(1000 * (minute - 30) * 3);
                    } else
                    {
                        await Coroutine.Sleep(3000);
                    }
                    break;
            }

            isDone = true;
            return true;
        }

    }
}
