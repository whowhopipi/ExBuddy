namespace ExBuddy.OrderBotTags.WhoFish
{
    using Clio.Utilities;
    using Clio.XmlEngine;
    using ff14bot.Enums;
    using ff14bot.Managers;
    using System;
    using System.ComponentModel;
    [XmlElement("HookAction")]
	public class HookAction
    {
        private Func<bool> func = null;

        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        public string Condition { set; get; }

        [DefaultValue(true)]
        [XmlAttribute("UseSkill")]
        public bool UseSkill { set; get; }

        [XmlAttribute("TugType")]
        public TugType TugType { get; set; }

        [DefaultValue(-1)]
        [XmlAttribute("MoochEqual")]
        public int MoochEqual { get; set; }

        [DefaultValue(-1)]
        [XmlAttribute("MoochGreater")]
        public int MoochGreater { get; set; }

        [DefaultValue(-1)]
        [XmlAttribute("MoochLess")]
        public int MoochLess { get; set; }

        public bool IsResult(int moochLevel)
        {
            if(TugType != (TugType)0 && TugType != FishingManager.TugType)
            {
                return false;
            }

            if(MoochEqual != -1 && !(moochLevel == MoochEqual))
            {
                return false;
            } 

            if(MoochGreater != -1 && !(moochLevel > MoochGreater))
            {
                return false;
            }

            if(MoochLess != -1 && !(moochLevel < MoochLess))
            {
                return false;
            }

            if(func == null)
            {
                func = ScriptManager.GetCondition(Condition);
            }
            return func();
        }

        public override string ToString()
        {
            return string.Format("Condition:{0},TugType:{1},MoochEqual:{2},MoochGreater:{3},MoochLess:{4}",
                Condition,TugType,MoochEqual,MoochGreater,MoochLess);
        }
    }
}