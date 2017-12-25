namespace ExBuddy.Data
{
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
    }
}