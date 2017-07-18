namespace ExBuddy.OrderBotTags.Objects
{
    using Clio.Utilities;
    using Clio.XmlEngine;
    using ExBuddy.Interfaces;
    using System;

    [XmlElement("GatherItem")]
	public class GatherItem : IConditionNamedItem
	{
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

        #endregion

        private Func<bool> condition;

        public override string ToString()
		{
			return this.DynamicToString();
		}
	}
}