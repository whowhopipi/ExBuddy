namespace ExBuddy.Interfaces
{
    using OrderBotTags.Objects;
    using System.Threading.Tasks;

    public interface ICraftOrder
    {
        string Name { get; }

        bool CanExecute(RecipeItem recipe);

        Task<bool> Execute();

        Task<bool> CheckSkills();

        bool CheckCp();
        
    }
}