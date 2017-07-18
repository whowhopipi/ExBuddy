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

    public class SqlData : SQLiteConnection
	{
#if RB_CN
		private const string DbFileName = "ExBuddy_CN.s3db";
#else
		private const string DbFileName = "ExBuddySB.s3db";
#endif
		public static readonly string DataFilePath;

		public static readonly SqlData Instance;
        public static readonly SqlData RecipeInstance;

        private readonly Dictionary<uint, MasterpieceSupplyDutyResult> masterpieceSupplyDutyCache;

		// TODO: look into what localizedDictionary does for us??
		private readonly Dictionary<uint, RequiredItemResult> requiredItemCache;
        private readonly Dictionary<int, RecipeItem> recipeItemCache;

        static SqlData()
		{
			var path = Path.Combine(Environment.CurrentDirectory, "Plugins\\ExBuddy\\Data\\" + DbFileName);
            var recipePath = Path.Combine(Environment.CurrentDirectory, "Plugins\\ExBuddy\\Data\\recipes.db3");

            if (File.Exists(path))
			{
				DataFilePath = path;
			}
			else
			{
				DataFilePath =
					Directory.GetFiles(PluginManager.PluginDirectory, "*" + DbFileName, SearchOption.AllDirectories).FirstOrDefault();
            }

            if (!File.Exists(recipePath))
            {
                recipePath =
                    Directory.GetFiles(PluginManager.PluginDirectory, "*" + "recipes.db3", SearchOption.AllDirectories).FirstOrDefault();
            }


            Instance = new SqlData(DataFilePath);
            RecipeInstance = new SqlData(recipePath);
        }

		internal SqlData(string path)
			: base(path)
		{
			masterpieceSupplyDutyCache = Table<MasterpieceSupplyDutyResult>().ToDictionary(key => key.Id, val => val);
			requiredItemCache = Table<RequiredItemResult>().ToDictionary(key => key.Id, val => val);
            recipeItemCache = Table<RecipeItem>().ToDictionary(key => key.Id, val => val);
        }

		public uint? GetIndexByEngName(string engName)
		{
			////			var result = Query<MasterpieceSupplyDutyResult>(@"
			////select m.*
			////from MasterpieceSupplyDutyResult m
			////join RequiredItemResult r on m.Id = r.MasterpieceSupplyDutyResultId
			////where r.EngName = ?", engName).SingleOrDefault();

			////			return result?.Index;

			var requiredItem =
				requiredItemCache.FirstOrDefault(
					kvp => string.Equals(kvp.Value.EngName, engName, StringComparison.OrdinalIgnoreCase)).Value;

			if (requiredItem == null)
			{
				return null;
			}

			MasterpieceSupplyDutyResult masterpieceSupplyDuty;
			if (!masterpieceSupplyDutyCache.TryGetValue(requiredItem.MasterpieceSupplyDutyResultId, out masterpieceSupplyDuty))
			{
				return null;
			}

			return masterpieceSupplyDuty.Index;
		}

		public uint? GetIndexByName(string name)
		{
			var requiredItem =
				requiredItemCache.FirstOrDefault(
					kvp => string.Equals(kvp.Value.CurrentLocaleName, name, StringComparison.OrdinalIgnoreCase)).Value;

			if (requiredItem == null)
			{
				return null;
			}

			MasterpieceSupplyDutyResult masterpieceSupplyDuty;
			if (!masterpieceSupplyDutyCache.TryGetValue(requiredItem.MasterpieceSupplyDutyResultId, out masterpieceSupplyDuty))
			{
				return null;
			}

			return masterpieceSupplyDuty.Index;
		}

		public uint? GetIndexByItemId(uint itemId)
		{
			////			var result = Query<MasterpieceSupplyDutyResult>(@"
			////select m.*
			////from MasterpieceSupplyDutyResult m
			////join RequiredItemResult r on m.Id = r.MasterpieceSupplyDutyResultId
			////where r.Id = ?", itemId).SingleOrDefault();

			////			return result?.Index;

			RequiredItemResult requiredItem;
			if (!requiredItemCache.TryGetValue(itemId, out requiredItem))
			{
				return null;
			}

			MasterpieceSupplyDutyResult masterpieceSupplyDuty;
			if (!masterpieceSupplyDutyCache.TryGetValue(requiredItem.MasterpieceSupplyDutyResultId, out masterpieceSupplyDuty))
			{
				return null;
			}

			return masterpieceSupplyDuty.Index;
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