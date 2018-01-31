namespace ExBuddy.Data
{
    using ExBuddy.Helpers;
    using ff14bot.Enums;
    using ff14bot.Managers;
    using OrderBotTags.Objects;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class RecipeSqlData : SQLiteConnection
	{
        public static readonly RecipeSqlData Instance;
        
        private readonly Dictionary<int, RecipeItem> recipeItemCache;

        private readonly Dictionary<int, CraftAction> craftActionsCache;

        static RecipeSqlData()
		{
            var recipePath = Path.Combine(Environment.CurrentDirectory, "Plugins\\ExBuddy\\Data\\recipes.db3");
            
            if (!File.Exists(recipePath))
            {
                recipePath =
                    Directory.GetFiles(PluginManager.PluginDirectory, "*" + "recipes.db3", SearchOption.AllDirectories).FirstOrDefault();
            }
            
            Instance = new RecipeSqlData(recipePath);
        }

		internal RecipeSqlData(string path)
			: base(path)
		{
            recipeItemCache = Table<RecipeItem>().ToDictionary(key => key.Id, val => val);
            craftActionsCache = Table<CraftAction>().ToDictionary(key => key.Id, val => val);
        }
        
        public RecipeItem GetRecipeByName(string RecipeName, ClassJobType Job)
        {
            Item item = DataManager.GetItem(RecipeName);

            if (item == null)
            {
                return null;
            }

            return recipeItemCache.Values.FirstOrDefault(recipe => recipe.ItemName.Equals(item.EnglishName) && recipe.ClassJob == Job);
        }

        public CraftAction GetCraftActionById(CraftActions action)
        {
            return craftActionsCache[(int)action];
        }

        public uint GetCraftActionId(CraftActions actions,ClassJobType job)
        {
            CraftAction action = GetCraftActionById(actions);

            uint retActionId = 0;

            switch (job)
            {
                case ClassJobType.Carpenter:
                    retActionId = action.Carpenter;
                    break;
                case ClassJobType.Blacksmith:
                    retActionId = action.Blacksmith;
                    break;
                case ClassJobType.Armorer:
                    retActionId = action.Armorer;
                    break;
                case ClassJobType.Goldsmith:
                    retActionId = action.Goldsmith;
                    break;
                case ClassJobType.Leatherworker:
                    retActionId = action.Leatherworker;
                    break;
                case ClassJobType.Weaver:
                    retActionId = action.Weaver;
                    break;
                case ClassJobType.Alchemist:
                    retActionId = action.Alchemist;
                    break;
                case ClassJobType.Culinarian:
                    retActionId = action.Culinarian;
                    break;
            }
            return retActionId;
        }
    }
}