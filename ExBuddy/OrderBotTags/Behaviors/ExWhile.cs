using Clio.Utilities;
using Clio.XmlEngine;
using ExBuddy.Logging;
using ff14bot.NeoProfiles;
using System;
using System.ComponentModel;
using TreeSharp;

namespace ExBuddy.OrderBotTags.Behaviors
{

    [XmlElement("ExWhile")]
    public class ExWhile : WhileTag
    {
        [DefaultValue(0)]
        [XmlAttribute("Loop")]
        public int Loop { set; get; }

        private int count = 0;
        private Logger logger = new Logger();

        private long lastTime = 0;

        private const long WAIT_TIME = 3000;

        protected override void OnStart()
        {
            logger.Verbose("执行OnStart()方法");

            base.OnStart();
        }
        
        public override bool IsDone
        {
            get
            {
                bool parentIsDone = base.IsDone;
                bool localIsDone = Loop != 0 && count++ >= Loop;
                logger.Verbose("是否完成：{0},{1},当前执行第{2}次", parentIsDone, localIsDone, count);

                long currentTime = DateTime.Now.Millisecond;

                long elapseTime = currentTime - lastTime;

                if(elapseTime < WAIT_TIME)
                {
//                    logger.Verbose("过快，暂停3秒");
                    Sleep sleep = new Sleep((int)(WAIT_TIME - elapseTime));
                    sleep.Start(this);
                }
                lastTime = DateTime.Now.Millisecond;

                return parentIsDone || localIsDone;
            }
        }
        
        protected override void OnDone()
        {
            logger.Verbose("执行OnDone()");
            base.OnDone();
            count = 0;
        }

        protected override void OnResetCachedDone()
        {
            logger.Verbose("执行OnResetCachedDone()");
            base.OnResetCachedDone();
        }

        /*
        protected override Composite CreateBehavior()
        {
            logger.Verbose("执行CreateBehavior()");
            logger.Verbose("第{0}次执行循环", count);
            return new Wait(3000, base.CreateBehavior());
        }*/
    }
}
