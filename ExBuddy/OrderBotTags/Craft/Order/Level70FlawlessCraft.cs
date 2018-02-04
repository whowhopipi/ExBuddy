namespace ExBuddy.OrderBotTags.Craft.Order
{
    using Helpers;
    using System.Threading.Tasks;
    using ff14bot.Managers;
    using ff14bot.Enums;
    using Behaviors.Objects;
    using ff14bot;
    using ff14bot.Objects;

    public class Level70FlawlessCraft : BaseCraftOrder
    {
        public override string Name
        {
            get
            {
                return "Level70FlawlessCraft";
            }
        }

        public override int MiniCp
        {
            get
            {
                return 405;
            }
        }
		
        public bool CanExecute(RecipeItem recipe)
        {
            return recipe.Level == 70;
        }
        
        public override async Task<bool> CheckSkills()
        {
            Logger.Info("需要技能：坚实的心得、安逸、内静、稳手、稳手2、渐进、坚实制作、简约2、集中加工、仓促、加工、秘诀、精修2、比尔格的祝福、阔步、模范制作2");
            return await checkSkill(CraftActions.MakersMark, "缺少技能：坚实的心得")
                && await checkSkill(CraftActions.ComfortZone,"缺少技能：安逸")
                && await checkSkill(CraftActions.InnerQuiet,"缺少技能：内静")
                && await checkSkill(CraftActions.SteadyHand, "缺少技能：稳手")
                && await checkSkill(CraftActions.SteadyHandII,"缺少技能：稳手2")
                && await checkSkill(CraftActions.PiecebyPiece, "缺少技能：渐进")
                && await checkSkill(CraftActions.FlawlessSynthesis, "缺少技能：坚实制作")
                && await checkSkill(CraftActions.TricksoftheTrade, "缺少技能：秘诀")
                && await checkSkill(CraftActions.ManipulationII, "缺少技能：掌握II")
                && await checkSkill(CraftActions.Observe, "缺少技能：观察")
                && await checkSkill(CraftActions.FocusedTouch, "缺少技能：注视加工")
                && await checkSkill(CraftActions.PrudentTouch, "缺少技能：简约加工")
                && await checkSkill(CraftActions.ByregotsBlessing,"缺少技能：比尔格的祝福")
                && await checkSkill(CraftActions.GreatStrides,"缺少技能：阔步")
                && await checkSkill(CraftActions.CarefulSynthesisII, "缺少技能：模范制作II");
        }
        
        private long TheoryCarefulSynthesisIITimes   // 理论上需要模范制作II的次数
        {
            get
            {
                if (LeftProcess - CarefulSynthesisIIProcess <= 0) return 1;
                long theoryLeftProcess = LeftProcess - (MakersMarkNums - 2) * FlawlessSynthesisProcess; // 保留2次坚实，做备用

                long tempTimes = theoryLeftProcess / CarefulSynthesisIIProcess;
                tempTimes += theoryLeftProcess % CarefulSynthesisIIProcess == 0 ? 0 : 1;

                Logger.Info("坚实的心得次数:{0}，模范制作II加工数:{1}，理论上还需要{2}次模范制作II",MakersMarkNums,CarefulSynthesisIIProcess,tempTimes);

                return tempTimes;
            }
        }

        private bool NeedTricksoftheTrade   // 秘诀
        {
            get
            {
				if(!IsGoodCondition)
                {
                    return false;
                }
                
                // 根据安逸剩余buff次数，每回合回复8cp，计算当前CP在回合数后能否回满
                int balanceCp = Core.Me.MaxCP - Core.Me.CurrentCP;
                return balanceCp > ComfortZoneNums * 8;
            }
        }

		private bool NeedComfortZone    // 安逸
        {
            get
            {
                return !(Core.Me.MaxCP < CurrentCP + 5 || MustFlawlessSynthesis || HasComfortZoneAura);
            }
        }

		private bool MustFlawlessSynthesis  // 是否必须使用坚实
        {
            get
            {
                // 预留2次坚实次数保底
                if (HasMakersMark)
                    return (LeftProcess - CarefulSynthesisIIProcess * TheoryCarefulSynthesisIITimes) > (MakersMarkNums - 2) * FlawlessSynthesisProcess;
                else
                    return false;
            }
        }

        private bool CanFlawlessSynthesis   // 坚实
        {
            get
            {
                return LeftProcess > FlawlessSynthesisProcess;
            }
        }

        private async Task<bool> DoFlawlessSynthesis()  // 使用坚实循环
        {
            if (NeedComfortZone)
            {
                await Cast(CraftActions.ComfortZone);
            } else if(NeedTricksoftheTrade)
            {
                await Cast(CraftActions.TricksoftheTrade);
            } else if(CanFlawlessSynthesis)
            {
                await Cast(CraftActions.FlawlessSynthesis);
            }

            return true;
        }

        private async Task<bool> DoComfortZoneAction(CraftActions action,bool flag)
        {
            if (flag && ComfortZoneNums <= 1) await DoAction(CraftActions.ComfortZone);
            else if(!HasComfortZoneAura) await DoAction(CraftActions.ComfortZone);
            await DoAction(action);
            return true;
        }

        private async Task<bool> doTricksoftheTrade()
        {
            if (NeedTricksoftheTrade)
            {
                await Cast(CraftActions.TricksoftheTrade);//秘诀
                return true;
            } else
            {
                return false;
            }
        }

        private async Task<bool> DoAction(CraftActions action)
        {
            bool flag = await doTricksoftheTrade();
            await Cast(action);
            return flag;
        }

        private async Task<bool> DoControl()
        {
            if(!HasManipulationII && ManipulationIITimes < 2)
            {
                await Cast(CraftActions.ManipulationII);
                ManipulationIITimes++;
            } else if(NeedComfortZone)
            {
                await Cast(CraftActions.ComfortZone);
            } else if(NeedTricksoftheTrade)
            {
                await Cast(CraftActions.TricksoftheTrade);
            } else if(CurrentCP <= endCp || CurrentCP - 25 < endCp)
            {
                return false;
            }
            else
            {
                await Cast(CraftActions.Observe);
                await Cast(CraftActions.FocusedTouch);
                currentControl++;
            }

            if(currentControl <= MaxControl && CurrentCP - endCp + 5 < 7 + 18)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> DoEndControl()
        {
            if (Core.Me.CurrentCP - 32 - 24 >= 21)
            {
                await Cast(CraftActions.PrudentTouch);
            }
            else if (Core.Me.CurrentCP - 32 - 24 >= 18)
            {
                await Cast(CraftActions.BasicTouch);
            }
            else if (Core.Me.CurrentCP - 32 - 24 >= 5)
            {
                await Cast(CraftActions.HastyTouchII);
            }

            return true;
        }

        private async Task<bool> DoEnd()
        {
            await Cast(CraftActions.SteadyHandII);
            await Cast(CraftActions.PrudentTouch);

            for(long i= currentControl - MaxControl; i<= 2;i++)
            {
                await DoEndControl();
            }

            if (CraftingManager.Condition == CraftingCondition.Excellent)
            {
                await Cast(CraftActions.ByregotsBlessing);
            } else
            {
                await Cast(CraftActions.GreatStrides);
                await Cast(CraftActions.ByregotsBlessing);
            }

            return true;
        }

        private int CarefulSynthesisIIProcess = 0;
        private int ManipulationIITimes = 0;
        private int currentControl = 0;

        private const int endCp = 102;
        private const int MaxControl = 6;

        public override async Task<bool> OnStart()
        {
            CarefulSynthesisIIProcess = 0;
            ManipulationIITimes = 0;
            currentControl = 0;
            return true;
        }
        
        public override async Task<bool> DoExecute()
        {
            CarefulSynthesisIIProcess = int.Parse(param);
			
            await Cast(CraftActions.MakersMark); //坚实的心得
            
            await DoAction(CraftActions.ComfortZone);     //安逸
            await DoAction(CraftActions.InnerQuiet);      //内静
            
            bool flag = true;
            await DoAction(CraftActions.SteadyHand); //稳手
            flag = flag && await DoAction(CraftActions.PiecebyPiece);//渐进
            flag = flag && await DoAction(CraftActions.PiecebyPiece);//渐进

            // 判断是否该用渐进
            if(LeftProcess / 3 > CarefulSynthesisIIProcess)
            {
                if (flag)
                {   // 如果前两次都触发了秘诀，那么最后一次就不管秘诀了
                    await DoAction(CraftActions.PiecebyPiece);//渐进
                } else
                {
                    await Cast(CraftActions.PiecebyPiece);
                }
            }

            // 获得坚实的心得剩余次数
            uint left = MakersMarkNums;
            
            for (int i = 0;i< left;i++)
            {
                await DoFlawlessSynthesis();
            }

            // 判断需要多少次模范制作II
            long CarefulSynthesisIITimes = LeftProcess / CarefulSynthesisIIProcess;
            CarefulSynthesisIITimes += LeftProcess % CarefulSynthesisIIProcess == 0 ? 0 : 1;

            Logger.Info("还剩余{0}进度，还需要推{1}次模范制作II", LeftProcess, CarefulSynthesisIITimes);

            for(long i= 1;i<CarefulSynthesisIITimes;i++)
            {
                await DoComfortZoneAction(CraftActions.CarefulSynthesisII,false);
            }

            // 加工循环
            while (await DoControl())
            {
            }

            // 收尾
            await DoEnd();
            
            while (IsCrafting())
            {
                await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
            }

            return true;
        }
        

    }
}