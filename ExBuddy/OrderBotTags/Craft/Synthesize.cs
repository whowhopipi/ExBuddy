namespace ExBuddy.OrderBotTags.Craft
{
    using Behaviors;
    using Buddy.Coroutines;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using Data;
    using ExBuddy.Helpers;
    using ff14bot;
    using ff14bot.Behavior;
    using ff14bot.Managers;
    using ff14bot.NeoProfiles;
    using ff14bot.Objects;
    using ff14bot.RemoteWindows;
    using Objects;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    [XmlElement("ExSynthesize")]
    public class Synthesize : ExProfileBehavior
    {
        [XmlAttribute("RecipeId")]
        public uint RecipeId { get; set; }

        [DefaultValue(0)]
        [XmlAttribute("CollectValue")]
        public int CollectValue { get; set; }

        [XmlAttribute("MiniCp")]
        public int MiniCp { get; set; }
                
        [XmlAttribute("HQMats")]
        public int[] HQMats { get; set; }

        [XmlElement("Actions")]
        internal List<string> Actions { set; get; }

        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        internal string Condition { set; get; }

        [DefaultValue(-1)]
        [XmlAttribute("Loop")]
        internal int Loop { set; get; }

        [XmlAttribute("OrderName")]
        public string OrderName { set; get; }

        [DefaultValue(0)]
        [XmlAttribute("FoodId")]
        public int FoodId { get; set; }

        [DefaultValue(0)]
        [XmlAttribute("MedicineId")]
        public int MedicineId { get; set; }

        [DefaultValue(true)]
        [XmlAttribute("HqFood")]
        public bool HqFood { set; get; }

        [XmlAttribute("Params")]
        public string Params { set; get; }

        private int num = 0;
        private int count = 0;
        private Func<bool> condition;

        private RecipeItem recipe;

        protected override void OnStart()
        {
            if(Actions == null)
            {
                Actions = new List<string>();
            }

            if (RecipeId == 0)
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    Logger.Verbose("开始查找配方{0}/{1}", Name, Me.CurrentJob);
                    recipe = RecipeSqlData.Instance.GetRecipeByName(Name,Me.CurrentJob);
                    if (recipe != null)
                        RecipeId = (uint)recipe.Id;
                }
            }

            if(HQMats == null || HQMats.Length == 0)
            {
                Logger.Info("HQMats为空，设置默认值");
                HQMats = new int[] { -2, -2, -2, -2, -2, -2 };
            } else
            {
                Logger.Verbose("HQMats不为空：" + HQMats);
            }

            condition = ScriptManager.GetCondition(Condition);
            count = 0;
            num = 0;
        }

        private async Task<bool> CloseWindow()
        {

            if (CraftingLog.IsOpen)
            {
                Log("Closing crafting window");
                CraftingLog.Close();
            }

            await Coroutine.Wait(10000, () => !CraftingLog.IsOpen);
            await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);

            isDone = true;

            return true;
        }
        
        protected bool IsCrafting()
        {
            return CraftingManager.IsCrafting && CraftingManager.ProgressRequired != CraftingManager.Progress;
        }

        protected override async Task<bool> DoMainFailed()
        {
            Logger.Verbose("执行失败了，我执行了一次");

            await CloseWindow();
            isDone = true;
            return true; 
        }

        private bool NeedCheck(AbilityAura aura)
        {
            // 如果没有，则补充
            if (!ExBuddy.Helpers.Actions.HasAura(aura)) return true;

            // 如果有，判断剩余时间，少于2分钟，补充
            Aura a = Core.Me.GetAuraById((uint)aura);
            return a.TimeLeft < 2*60;
        }

        protected override async Task<bool> Main()
        {
            Logger.Verbose("第{0}次执行",num ++);

            await CommonTasks.StopAndDismount();

            // 检查条件
            if (!condition())
            {
                Logger.Info("条件不合适，结束");
                await CloseWindow();
                return false;
            } else
            {
                Logger.Verbose("检查条件通过：{0}", Condition);
            }

            // 检查循环次数
            if(Loop > 0 && count >= Loop)
            {
                Logger.Info("循环次数达到最大{0}", count);
                await CloseWindow();
                return false;
            } else
            {
                Logger.Verbose("检查循环次数通过：{0}/{1}", count, Loop);
            }
            
            // 检查食物
            if (FoodId != 0)
            {
                // 检查是否需要吃食物
                if (NeedCheck(AbilityAura.Food))
                {
                    if (CraftingLog.IsOpen)
                    {
                        Log("Closing crafting window");
                        CraftingLog.Close();
                    }

                    await Coroutine.Wait(10000, () => !CraftingLog.IsOpen);
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);

                    // 判断是否有食物
                    var validItems = InventoryManager.FilledSlots.Where(r => r.RawItemId == FoodId && r.IsHighQuality == HqFood).ToArray();

                    if(validItems.Length == 0)
                    {
                        Logger.Warn("没有食物{0}({1})",FoodId,(HqFood ? "HQ":"NQ"));
                        await CloseWindow();
                        return false;
                    }

                    var item = validItems.FirstOrDefault();
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => item.CanUse(null));
                    item.UseItem();
                    await Coroutine.Sleep(5000);
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => ExBuddy.Helpers.Actions.HasAura(AbilityAura.Food));
                }
            }

            // 检查药水
            if (MedicineId != 0)
            {
                // 检查是否需要吃药水
                if (NeedCheck(AbilityAura.Medicated))
                {
                    if (CraftingLog.IsOpen)
                    {
                        Log("Closing crafting window");
                        CraftingLog.Close();
                    }

                    await Coroutine.Wait(10000, () => !CraftingLog.IsOpen);
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);

                    // 判断是否有药水
                    var validItems = InventoryManager.FilledSlots.Where(r => r.RawItemId == MedicineId && r.IsHighQuality == HqFood).ToArray();

                    if (validItems.Length == 0)
                    {
                        Logger.Warn("没有药水{0}({1})", MedicineId, (HqFood ? "HQ" : "NQ"));
                        await CloseWindow();
                        return false;
                    }

                    var item = validItems.FirstOrDefault();
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => item.CanUse(null));
                    item.UseItem();
                    await Coroutine.Sleep(5000);
                    await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => ExBuddy.Helpers.Actions.HasAura(AbilityAura.Food));
                }
            }

            // 检查配方ID
            if (RecipeId == 0)
            {
                Logger.Error("获取配方失败,{0}",Name);
                await CloseWindow();
                return false;
            }
            Logger.Verbose("查找配方通过，{0}", RecipeId);
                
            // 设置收藏品技能
            if(CollectValue > 0)
            {
                if(!ExBuddy.Helpers.Actions.HasAura(AbilityAura.CollectorsMake) && ExBuddy.Helpers.Actions.CanCast(Ability.CollectorsGlove))
                {
                    await ExBuddy.Helpers.Actions.CastAura(Ability.CollectorsGlove,300, AbilityAura.CollectorsMake);
                    await Coroutine.Sleep(2000);
                }
            } else
            {
                if (ExBuddy.Helpers.Actions.HasAura(AbilityAura.CollectorsMake) && ExBuddy.Helpers.Actions.CanCast(Ability.CollectorsGlove))
                {
                    await ExBuddy.Helpers.Actions.CastAura(Ability.CollectorsGlove,300);
                    await Coroutine.Sleep(2000);
                }
            }

            // 设置配方
            if (CraftingManager.CurrentRecipeId != RecipeId)
            {
                Logger.Verbose("设置配方{0}", RecipeId);
                if (!await CraftingManager.SetRecipe(RecipeId))
                {
                    LogError("We don't know the required recipe({0} - {1}).",RecipeId,Name  );
                    await CloseWindow();
                    return false;
                } else
                {
                    Logger.Verbose("设置配方{0}成功", RecipeId);
                }
            }

            await Coroutine.Sleep(500);

            RecipeData contents = CraftingManager.CurrentRecipe;

            BaseCraftOrder craftOrder = null;
            Logger.Verbose("开始查找执行器");
            if(Actions != null && Actions.Count > 0)
            {
                Logger.Verbose("技能列表不为空，查找自定义执行器");
                craftOrder = CraftOrderManager.NewCustomOrder(Actions, MiniCp);
            } else if (!string.IsNullOrWhiteSpace(OrderName))
            {
                Logger.Verbose("执行器名称({0})不为空",OrderName);
                craftOrder = CraftOrderManager.GetOrderByName(OrderName);
            } else
            {
                Logger.Verbose("查找其他执行器");
                craftOrder = CraftOrderManager.GetOrder(recipe);
            }

            if(craftOrder == null)
            {
                Logger.Info("没有找到执行器，结束");
                isDone = true;
                return false;
            } else
            {
                Logger.Info("执行器已找到，开始执行.");
            }

            craftOrder.Logger = Logger;
            craftOrder.recipe = recipe;
            craftOrder.param = Params;

            Logger.Verbose("技能检查开始");
            if (!await craftOrder.CheckSkills())
            {
                await CloseWindow();
                return false;
            }

            Logger.Verbose("制作力检查开始");
            if (!craftOrder.CheckCp())
            {
                await CloseWindow();
                return false;
            }

            // 判断素材是否足够
            Logger.Verbose("判断素材是否足够");
            for (int i = 0; i < contents.Ingredients.Length; i++)
            {
                RecipeIngredientInfo one = contents.Ingredients[i];

                if (one.ItemId == 0)
                {
                    break;
                }

                int currentHas = ConditionParser.ItemCount(one.ItemId);
                if (currentHas < one.TotalNeeded)
                {
                    Logger.Error("物品{0}不足，{1}/{2}", one.ItemId, currentHas, one.TotalNeeded);
                    await CloseWindow();
                    return false;
                }

            }

            await CraftingManager.SetQuality(HQMats);

            //Start the crafting
            if (CraftingLog.IsOpen)
            {
                await Coroutine.Sleep(1000);
                if (CraftingManager.CanCraft)
                {
                    var itemData = DataManager.GetItem(contents.ItemId);
                    Logger.Info("Crafting {0} ({1}) via {2}", itemData.CurrentLocaleName, contents.ItemId, RecipeId);
                    CraftingLog.Synthesize();
                }
                else
                {
                    LogError("Cannot craft, perhaps we are out of materials?");
                    await CloseWindow();
                    return false;
                }
            }
            

            await Coroutine.Yield();
            await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => CraftingManager.IsCrafting);
            await Coroutine.Wait(Timeout.InfiniteTimeSpan, () => !CraftingManager.AnimationLocked);

            StatusText = string.Format("制作：{0}({1})，第{2}次", RecipeId, Name, (count+1));

            if (await craftOrder.Execute())
            {
                count++;
            }
            else
            {
                await CloseWindow();
                return false;
            }

            if (IsCrafting())
            {
                Logger.Error("制作未完成，结束");
                return false;
            }

            if (SelectYesNoItem.IsOpen)
            {
                if (SelectYesNoItem.CollectabilityValue >= CollectValue)
                {
                    SelectYesNoItem.Yes();
                }
                else
                {
                    SelectYesNoItem.No();
                }
                await Coroutine.Wait(10000, () => !SelectYesNoItem.IsOpen);
                await Coroutine.Wait(Timeout.Infinite, () => !CraftingManager.AnimationLocked);
                await Coroutine.Sleep(250);
            }

            return true;
        }
        
    }
}