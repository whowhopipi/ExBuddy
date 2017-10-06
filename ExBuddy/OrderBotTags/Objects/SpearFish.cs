namespace ExBuddy.OrderBotTags.Objects
{
    using System;
    using Clio.XmlEngine;
    using Interfaces;
    using Clio.Utilities;

    [XmlElement("SpearFish")]
    public class SpearFish : IConditionNamedItem
    {
        public override string ToString() { return this.DynamicToString(); }

        #region INamedItem Members

        [XmlAttribute("Id")]
        public uint Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("LocalName")]
        public string LocalName { get; set; }

        [XmlAttribute("Condition")]
        public string Condition { get; set; }

        public bool ConditionResult
        {
            get
            {
                if (Condition == null || Condition.Equals(""))
                    return true;

                if (condition == null)
                {
                    condition = ScriptManager.GetCondition(Condition);
                }
                return condition();
            }
        }

        private Func<bool> condition;

        #endregion INamedItem Members
    }
}