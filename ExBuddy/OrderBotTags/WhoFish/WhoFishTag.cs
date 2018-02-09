namespace ExBuddy.OrderBotTags.WhoFish
{
    using Buddy.Coroutines;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using Enumerations;
    using ExBuddy.Attributes;
    using ExBuddy.Helpers;
    using ExBuddy.OrderBotTags.Behaviors;
    using ExBuddy.OrderBotTags.Fish;
    using ExBuddy.OrderBotTags.Gather;
    using ExBuddy.OrderBotTags.Objects;
    using ff14bot;
    using ff14bot.Enums;
    using ff14bot.Managers;
    using ff14bot.RemoteWindows;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [LoggerName("WhoFish")]
    [XmlElement("WhoFish")]
    public class WhoFishTag : ExProfileBehavior
    {

        [DefaultValue("True")]
        [XmlAttribute("Condition")]
        public string Condition { set; get; }

        [DefaultValue(200)]
        [XmlAttribute("BaitDelay")]
        public int BaitDelay { get; set; }

        [DefaultValue(false)]
        [XmlAttribute("Patience")]
        public bool Patience { get; set; }

        [DefaultValue(false)]
        [XmlAttribute("Patience2")]
        public bool Patience2 { set; get; }

        [DefaultValue(false)]
        [XmlAttribute("FishEyes")]
        public bool FishEyes { set; get; }

        [DefaultValue(false)]
        [XmlAttribute("Snagging")]
        public bool Snagging { set; get; }

        [DefaultValue(false)]
        [XmlAttribute("Stealth")]
        public bool Stealth { get; set; }

        [XmlElement("Baits")]
        public List<ExBuddy.OrderBotTags.Fish.Bait> Baits { get; set; }

        [XmlElement("Items")]
        public NamedItemCollection Items { get; set; }

        [XmlElement("Keepers")]
        public List<Keeper> Keepers { get; set; }

        [XmlElement("Collectables")]
        public List<Collectable> Collectables { get; set; }

        [XmlElement("FishSpots")]
        public IndexedList<WhoFishSpot> FishSpots { get; set; }

        [XmlElement("MoochFishs")]
        public List<string> MoochFishs { get; set; }

        [XmlElement("HookActions")]
        public List<HookAction> HookActions { get; set; }

        [DefaultValue(false)]
        [XmlAttribute("Loop")]
        public bool Loop { set; get; }
#if RB_CN
        protected static Regex FishRegex = new Regex(@"[\u4e00-\u9fa5]+钓上了|[\u4e00-\u9fa5]+",RegexOptions.Compiled | RegexOptions.IgnoreCase);
#else
        protected static Regex FishRegex = new Regex(@"You land(?: a| an)? (.+) measuring (\d{1,4}\.\d) ilms!", RegexOptions.Compiled | RegexOptions.IgnoreCase);
#endif
        protected static Regex FishSizeRegex = new Regex(@"(\d{1,4}\.\d)",RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly ExBuddy.Windows.Bait baitWindow = new ExBuddy.Windows.Bait();

        private Func<bool> condition;
        private FishResult result;
        private WhoFishSpot currentFishSpot;
        private int moochLevel;

        private FishingState currentState;

        private bool firstWaitin = true;

        protected override async Task<bool> Main()
        {
            currentState = FishingManager.State;
//            Logger.Verbose("当前状态：{0}", currentState);

            bool flag = true;

            switch (currentState)
            {
                case FishingState.None:// 不处于钓鱼状态
                    if (   await CheckCondition()
                        && await MoveToNextSport() 
                        && await UseCollectors()
                        && await HandleBait()
                        && await UseStealth()
                        && await UseSnagging()
                        && await UseFishEyes()
                        && await UsePatience()
                        && await HandleCast())
                    {
                        flag = true;
                    } else
                    {
                        return false;
                    }
                    break;
                case FishingState.Bite: // 鱼咬钩了
                    Logger.Verbose("鱼咬钩了，当前鱼竿状态：{0}，以小钓大次数：{1}", FishingManager.TugType, moochLevel);
                    flag = await HandHook();
                    break;
                case (FishingState)7:   // 放生中
                    result = null;
                    break;
                case (FishingState)9:   // 钓鱼中
                    result = null;
                    firstWaitin = true;
                    if (currentFishSpot.FishType == FishStrategy.TouchAndGo)
                    {
                        ChangeNextFishSpot();

                        await Actions.Cast(FishActions.Quit,SpellDelay);
                        await Coroutine.Wait(5000, () => WorldManager.CanTeleport());
                    }
                    break;
                case FishingState.PoleReady:    // 已经钓上了鱼，等待中
                    if(await HandleMoock() && await HandleRelease())
                    {
                        if (!await CheckCondition())
                        {
                            await Actions.Cast(FishActions.Quit,SpellDelay);
                            await Coroutine.Wait(5000, () => WorldManager.CanTeleport());
                        } else if (   await MoveToNextSport()
                            && await HandleBait()
                            && await UseCollectors()
                            && await UseStealth()
                            && await UseSnagging()
                            && await UseFishEyes()
                            && await UsePatience()
                            && await HandleCast())
                        {
                            flag = true;
                        }
                        else
                        {
                            result = null;
                            return false;
                        }
                    }
                    break;
                case FishingState.Waitin:
                    if (!await HandleCollect())
                    {
                        return true;
                    }
                    break;
                case FishingState.Quit:
                    ChangeNextFishSpot();
                    break;
            }

            await Coroutine.Wait(10000, () => currentState != FishingManager.State);

            return flag;
        }
        
        protected override void OnStart()
        {
            SpellDelay = SpellDelay < 0 ? 0 : SpellDelay;

            if (Baits == null)
            {
                Baits = new List<ExBuddy.OrderBotTags.Fish.Bait>();
            }

            if(MoochFishs == null)
            {
                MoochFishs = new List<string>();
            }

            if(Keepers == null)
            {
                Keepers = new List<Keeper>();
            }

            if(HookActions == null)
            {
                HookActions = new List<HookAction>();
            } else
            {
                Logger.Verbose("提钩条件不为空，内容如下：");
                foreach(HookAction action in HookActions)
                {
                    Logger.Verbose("One HookAction is {0}", action);
                }
            }

            if(FishSpots == null)
            {
                FishSpots = new IndexedList<WhoFishSpot>();
                FishSpots.Add(new WhoFishSpot() {
                    NodeLocation = Core.Me.Location ,
                    Heading = Core.Me.Heading,
                    FishType = FishStrategy.GatherOrCollect,
                    Radius = 0.5f
                });
                currentFishSpot = FishSpots.Current;
            } else
            {
                Logger.Verbose("钓鱼点不为空，内容如下：");
                foreach(WhoFishSpot spot in FishSpots)
                {
                    Logger.Verbose("一个钓鱼点：{0}", spot);
                }
                currentFishSpot = null;
                FishSpots.Index = 0;
            }

            FishSpots.IsCyclic = Loop;

            if(Collectables == null)
            {
                Collectables = new List<Collectable>();
            } else
            {
                Logger.Verbose("收藏品不为空，内容如下：");
                foreach (Collectable spot in Collectables)
                {
                    Logger.Verbose("一个收藏品：{0}", spot);
                    Keepers.Add(new Keeper() { Action=KeeperAction.KeepAll,Name=spot.Name});
                }
                currentFishSpot = null;
                
            }

            GamelogManager.MessageRecevied += ReceiveMessage;

            Logger.Info("开始钓鱼");

            GamelogManager.MessageRecevied += ReceiveMessage;

            condition = ScriptManager.GetCondition(Condition);
        }

        protected override void OnDone()
        {
            if(FishingManager.State != FishingState.None)
            {
                ActionManager.DoAction(FishActions.Quit,Core.Me);
            }

			try
			{
				GamelogManager.MessageRecevied -= ReceiveMessage;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}
        }

        private async Task<bool> MoveToNextSport()
        {
            if(currentFishSpot == FishSpots.Current)
            {
                return true;
            }

            Logger.Info("前往下一个钓鱼点");

            if(currentState != FishingState.None)
            {
                await Actions.Cast(FishActions.Quit,SpellDelay);
                await Coroutine.Wait(5000, () => WorldManager.CanTeleport());
                return false;
            }

            result = null;

            await FishSpots.Current.MoveToSpot(this);

            // 设置面向
            Core.Me.SetFacing(FishSpots.Current.Heading);

            currentFishSpot = FishSpots.Current;

            return false;
        }

        private async Task<bool> CheckCondition()
        {
//            Logger.Verbose("检查条件");

            if(condition == null)
            {
                condition = ScriptManager.GetCondition(Condition);
            }

            if (condition())
            {
                return true;
            } else
            {
                Logger.Info("条件不满足，钓鱼结束");

                if (currentState != FishingState.None)
                {
                    await Actions.Cast(FishActions.Quit,SpellDelay);
                    await Coroutine.Wait(5000, () => WorldManager.CanTeleport());
                    return false;
                }

                isDone = true;
                return false;
            }
        }

        // 处理鱼饵
        private async Task<bool> HandleBait()
        {
//            Logger.Verbose("设置鱼饵");

            if (Baits.Count == 0)
            {
//                Logger.Warn("没有配置鱼饵");
                if (FishingManager.SelectedBaitItemId <= 0)
                {
                    Logger.Error("没有默认鱼饵，结束");
                    isDone = true;
                    return false;
                } else
                {
                    var BaitItem = DataManager.ItemCache[FishingManager.SelectedBaitItemId];
                    if(BaitItem == null)
                    {
                        Logger.Error("物品栏中没有这种鱼饵[{0}]，结束。",FishingManager.SelectedBaitItemId);
                        isDone = true;
                        return false;
                    } else
                    {
//                        Logger.Verbose("使用默认鱼饵");
                        return true;
                    }
                }
            }

            ExBuddy.OrderBotTags.Fish.Bait bait = ExBuddy.OrderBotTags.Fish.Bait.FindMatch(Baits);

            if(bait == null)
            {
                Logger.Error("当前没有符合条件的鱼饵，结束");
                isDone = true;
                return false;
            }

            // 判断是否需要更换鱼饵
            if(bait.BaitItem.Id != FishingManager.SelectedBaitItemId)
            {
                if (!await baitWindow.SelectBait(bait.BaitItem.Id, (ushort)BaitDelay))
                {
                    Logger.Error("钓饵选择错误");
                    isDone = true;
                    return false;
                }
            }
            return true;
        }

        // 处理抛竿
        private async Task<bool> HandleCast()
        {
//            Logger.Verbose("抛竿");

            //await Cast(Ability.Cast);
            FishingManager.Cast();

            await Coroutine.Sleep(200);

            moochLevel = 0;
            return false;
        }
        
        private bool ChangeNextFishSpot()
        {
            if(FishSpots.Current == currentFishSpot)
            {
                if (!FishSpots.Next())
                {
                    if (Loop)
                    {
                        FishSpots.Index = 0;
                    } else
                    {
                        Logger.Info("所有钓鱼点都已完成，结束");
                        isDone = true;
                    }
                }
            }

            return true;
        }

        private async Task<bool> HandleCollect()
        {
            if (!HasCollectorsGlove)
            {
//                Logger.Verbose("没有收藏品BUFF");
                return true;
            }

            if(!firstWaitin)
            {
//                Logger.Verbose("第二次进入，返回");
                return true;
            }

            if (!SelectYesNoItem.IsOpen)
            {
                var opened = await Coroutine.Wait(5000, () => SelectYesNoItem.IsOpen);
                if (!opened)
                {
                    Logger.Verbose("收藏品确认框未打开");
                    return true;
                }
            }

            Logger.Verbose("收藏值窗口打开，当前鱼{0}/{1}收藏值为：{2}",SelectYesNoItem.Item.CurrentLocaleName,SelectYesNoItem.Item.EnglishName,SelectYesNoItem.CollectabilityValue);

            var collect = Collectables.FirstOrDefault(c => c.ConditionResult && 
                (string.Equals(c.Name, SelectYesNoItem.Item.CurrentLocaleName,StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(c.Name,SelectYesNoItem.Item.EnglishName,StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(c.LocalName,SelectYesNoItem.Item.CurrentLocaleName,StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(c.LocalName,SelectYesNoItem.Item.EnglishName,StringComparison.InvariantCultureIgnoreCase)
                ) && SelectYesNoItem.CollectabilityValue >= c.Value);
    
            if (collect == null)
            {
//                Logger.Verbose("没找到符合条件的收藏品设置，不收藏该鱼");
                SelectYesNoItem.No();
                await Coroutine.Wait(3000, () => !SelectYesNoItem.IsOpen);
                return true;
            } else
            {
//                Logger.Verbose("找到符合条件的收藏品设置，收藏该鱼");
                SelectYesNoItem.Yes();
                result = null;
                firstWaitin = false;
                await Coroutine.Wait(3000, () => !SelectYesNoItem.IsOpen);
                return false;
            }
        }

        // 处理以小钓大
        private async Task<bool> HandleMoock()
        {
            await Coroutine.Sleep(200);
            //            Logger.Verbose("以小钓大");

            if (result == null)
            {
//                Logger.Verbose("当前没有钓上任何鱼,不能以小钓大");
                return true;
            }

            if (!FishingManager.CanMooch)
            {
//                Logger.Verbose("当前鱼不能进行以小钓大");
                return true;
            }

            if(MoochFishs.Count != 0)
            {
                if(!MoochFishs.Exists(f => string.Equals(f,result.FishName,StringComparison.InvariantCultureIgnoreCase)))
                {
//                    Logger.Verbose("当前鱼不在以小钓大列表中，不用进行以小钓大");
                    return true;
                }
            } else
            {
//                Logger.Verbose("以小钓大列表为空，所有符合条件的鱼都可以进行以小钓大");
            }

            FishingManager.Mooch();
            moochLevel++;

            return false;
        }

        // 处理放生
        private async Task<bool> HandleRelease()
        {
//            Logger.Verbose("放生");

            if(result == null)
            {
//                Logger.Verbose("当前没有钓上任何鱼，不需要放生");
                return true;
            }

            if (!Actions.CanCast(FishActions.Release))
            {
//                Logger.Verbose("放生技能不可用，可能已经被放生");
                await Coroutine.Sleep(1000);
                return true;
            } else if(Keepers.Count != 0)
            {
                if(Keepers.Any(result.IsKeeper))
                {
//                    Logger.Verbose("在保留列表中，不放生");
                } else
                {
//                    Logger.Verbose("不在保留列表中，放生");
                    await Actions.Cast(FishActions.Release,SpellDelay);

                    return false;
                }
            } else
            {
//                Logger.Verbose("保留列表为空，保留所有鱼");
            }

            return true;
        }

        // 处理提钩
        private async Task<bool> HandHook()
        {
//            Logger.Verbose("提钩");

            if (HookActions.Count != 0)
            {
                HookAction action = HookActions.FirstOrDefault<HookAction>(ac => ac.IsResult(moochLevel));

                if (action != null)
                {
                    uint hookset;
                    if (action.UseSkill)
                    {
                        hookset = FishingManager.TugType == TugType.Light ? FishActions.PrecisionHookset : FishActions.PowerfulHookset;
                    } else
                    {
                        hookset = FishActions.Hook;
                    }
                    if (Actions.CanCast(hookset))
                    {
//                        if(hookset == Ability.PrecisionHookset)
//                           Logger.Verbose("使用精准提钩");
//                        if (hookset == Ability.PowerfulHookset)
//                            Logger.Verbose("使用强力提钩");

                        await Actions.Cast(hookset,SpellDelay);
                    } else
                    {
                        await Actions.Cast(FishActions.Hook,SpellDelay);
                    }
                }
            }
            else
            {
                await Actions.Cast(FishActions.Hook,SpellDelay);
            }

            return false;
        }

        private void ReceiveMessage(object sender, ChatEventArgs e)
        {
            if (e.ChatLogEntry.MessageType == (MessageType)2115 && (e.ChatLogEntry.Contents.Contains("警惕性很高") || e.ChatLogEntry.Contents.Contains("The fish sense something amiss")))
            {
                Logger.Error("当前渔点警惕了，需要更换渔点");
                ChangeNextFishSpot();
            }
#if RB_CN
            else if (e.ChatLogEntry.MessageType == (MessageType)2115 && e.ChatLogEntry.Contents.Contains("成功钓上了"))
#else
            else if (e.ChatLogEntry.MessageType == (MessageType)2115 && e.ChatLogEntry.Contents.StartsWith("You land"))
#endif
            {
                SetFishResult(e.ChatLogEntry.Contents);
            }
        }

        protected void SetFishResult(string message)
        {
            var fishResult = new FishResult();
#if RB_CN
            var match = FishRegex.Matches(message);
            var sizematch = FishSizeRegex.Match(message);

            if (sizematch.Success)
            {
                fishResult.Name = match[1].ToString();
                float size;
                float.TryParse(sizematch.Groups[1].Value, out size);
#else
            var match = FishRegex.Match(message);
            if (match.Success)
            {
                fishResult.Name = match.Groups[1].Value;
                float size;
                float.TryParse(match.Groups[2].Value, out size);
#endif
                fishResult.Size = size;
                if (fishResult.Name[fishResult.Name.Length - 2] == ' ')
                {
                    fishResult.IsHighQuality = true;
                }
            }
            result = fishResult;
            Logger.Info("钓上来了：" + (result.IsHighQuality ? "HQ：" : "") + "{0}， 尺寸：{1}星寸", result.Name, result.Size);
        }

        int last = DateTime.Now.Second;

        internal bool MovementStopCallback(float distance, float radius)
        {
            int now = DateTime.Now.Second;

            if(now - last > 3)
            {
                last = now;
                return distance <= radius || !condition() ||ExProfileBehavior.Me.IsDead;
            } else
            {
                return distance <= radius || ExProfileBehavior.Me.IsDead;
            }
        }

#region Use Buff
        private bool Collectable
        {
            get
            {
                return Collectables.Count != 0;
            }
        }

        private async Task<bool> UseCollectors()
        {
            if (Collectable && !HasCollectorsGlove)
            {
                Logger.Info("使用收藏品");
                await Actions.CastAura(FishActions.Collect, SpellDelay,(int)AbilityAura.CollectorsGlove);
            }
            else if (!Collectable && HasCollectorsGlove)
            {
                Logger.Info("取消收藏品");
                await Actions.CastAura(FishActions.Collect, SpellDelay);
            }
            return true;
        }

        private async Task<bool> UseSnagging()
        {
            if (Snagging && !HasSnagging)
            {
                Logger.Info("使用钓组");
                await Actions.CastAura(FishActions.Snagging, SpellDelay ,(int)AbilityAura.Snagging);
            } else if(!Snagging && HasSnagging)
            {
                Logger.Info("取消钓组");
                await Actions.CastAura(FishActions.Snagging,SpellDelay);
            }
            return true;
        }

        private async Task<bool> UseFishEyes()
        {
            if (FishEyes && !HasFishEyes)
            {
                if (Actions.CanCast(FishActions.FishEyes))
                {
                    Logger.Info("使用鱼眼");
                    await Actions.CastAura(FishActions.FishEyes, SpellDelay,(int)AbilityAura.FishEyes);
                } else
                {
                    await Coroutine.Sleep(3000);
                    return false;
                }
            }
            return true;
        }

        private async Task<bool> UsePatience()
        {
            if (!FishEyes && (Patience || Patience2) && !HasPatience)
            {
                if(Patience2 && Actions.CanCast(FishActions.Patience2))
                {
                    Logger.Info("使用耐心2");
                    await Actions.CastAura(FishActions.Patience2, SpellDelay,(int)AbilityAura.Patience);
                } else if(Patience && Actions.CanCast(FishActions.Patience))
                {
                    Logger.Info("使用耐心");
                    await Actions.CastAura(FishActions.Patience, SpellDelay,(int)AbilityAura.Patience);
                }
            }
            return true;
        }

        private async Task<bool> UseStealth()
        {
            if (Stealth && !HasStealth)
            {
                Logger.Info("启用隐身");
                await Actions.CastAura(FishActions.Stealth, SpellDelay,(int)AbilityAura.Stealth);
            } else if(!Stealth && HasStealth)
            {
                Logger.Info("关闭隐身");
                await Actions.CastAura(FishActions.Stealth, SpellDelay);
            }
            return true;
        }

#endregion Use Buff

#region Aura Properties

        protected bool HasPatience
        {
            get
            {
                // Gathering Fortune Up (Fishing)
                return ExProfileBehavior.Me.HasAura(850);
            }
        }

        protected bool HasSnagging
        {
            get
            {
                // Snagging
                return ExProfileBehavior.Me.HasAura(761);
            }
        }

        protected bool HasCollectorsGlove
        {
            get
            {
                // Collector's Glove
                return ExProfileBehavior.Me.HasAura(805);
            }
        }

        protected bool HasChum
        {
            get
            {
                // Chum
                return ExProfileBehavior.Me.HasAura(763);
            }
        }

        protected bool HasFishEyes
        {
            get
            {
                // Fish Eyes
                return ExProfileBehavior.Me.HasAura(762);
            }
        }

        protected bool HasStealth
        {
            get
            {
                return ExProfileBehavior.Me.HasAura(47);
            }
        }

#endregion
    }
}
