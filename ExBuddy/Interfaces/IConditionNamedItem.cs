namespace ExBuddy.Interfaces
{
	public interface IConditionNamedItem
	{
		uint Id { get; set; }

		string Name { get; set; }

		string LocalName { get; set; }

        string Condition { get; set; }

        bool ConditionResult { get; }
    }
}