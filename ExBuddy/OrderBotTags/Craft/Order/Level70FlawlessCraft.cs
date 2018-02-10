namespace ExBuddy.OrderBotTags.Craft.Order
{
    using Helpers;
    using System.Threading.Tasks;
    using ff14bot.Managers;
    using ff14bot.Enums;
    using Behaviors.Objects;
    using ff14bot;
    using ff14bot.Objects;
    using System.Collections.Generic;
    using Buddy.Coroutines;

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

        public override List<CraftActions> NeedSkills()
        {
            List<CraftActions> needActions = new List<CraftActions>();
            needActions.Add(CraftActions.MakersMark);
            needActions.Add(CraftActions.InnerQuiet);
            needActions.Add(CraftActions.SteadyHand);
            needActions.Add(CraftActions.SteadyHandII);
            //needActions.Add(CraftActions.Ingenuity);
            needActions.Add(CraftActions.IngenuityII);
            needActions.Add(CraftActions.Innovation);
            needActions.Add(CraftActions.PiecebyPiece);
            needActions.Add(CraftActions.FlawlessSynthesis);
            needActions.Add(CraftActions.TricksoftheTrade);
            needActions.Add(CraftActions.ManipulationII);
            needActions.Add(CraftActions.Observe);
            needActions.Add(CraftActions.FocusedTouch);
            needActions.Add(CraftActions.PrudentTouch);
            needActions.Add(CraftActions.ByregotsBlessing);
            needActions.Add(CraftActions.GreatStrides);
            needActions.Add(CraftActions.CarefulSynthesisII);
            return needActions;
        }

        private long TheoryCarefulSynthesisIITimes   // 理论上需要模范制作II的次数
        {
            get
            {
                if (LeftProcess - FocusedSynthesisProcess <= 0) return 1;
                uint tempFlawlessSynthesisTimes = MakersMarkNums > 2 ? MakersMarkNums - 2 : MakersMarkNums;// 保留2次坚实，做备用

                long theoryLeftProcess = LeftProcess - tempFlawlessSynthesisTimes * FlawlessSynthesisProcess; 

                long tempTimes = theoryLeftProcess / FocusedSynthesisProcess;
                tempTimes += theoryLeftProcess % FocusedSynthesisProcess == 0 ? 0 : 1;

                Logger.Info("坚实的心得次数:{0}，新颖II下注视制作数:{1}，理论上还需要{2}次注视制作", MakersMarkNums, FocusedSynthesisProcess, tempTimes);

                return tempTimes;
            }
        }

        private long TheoryFlawlessSynthesisTimes   // 理论上还可以推的坚实制作数
        {
            get
            {
                long TheoryTimes = LeftProcess / FlawlessSynthesisProcess;
                TheoryTimes -= LeftProcess % FlawlessSynthesisProcess == 0 ? 0 : 1;
                if (TheoryTimes > MakersMarkNums) return MakersMarkNums;
                else return TheoryTimes;
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
                
                if(HasMakersMark)
                {
                    // 如果是在坚实循环中
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
                if(HasMakersMark)
                {
                    // 有坚实心得BUFF的时候
                    if (MustFlawlessSynthesis) return false;
                    if (HasComfortZoneAura) return false;
                    if (TheoryFlawlessSynthesisTimes <= 9) return true;
                } else if(HasComfortZoneAura)
                {
                    return false;
                }
                return Core.Me.MaxCP > CurrentCP + 5;
            }
        }

		private bool MustFlawlessSynthesis  // 是否必须使用坚实
        {
            get
            {
                // 预留2次坚实次数保底
                if (HasMakersMark)
                    return (LeftProcess - FocusedSynthesisProcess * TheoryCarefulSynthesisIITimes) > (MakersMarkNums - 2) * FlawlessSynthesisProcess;
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

        private async Task<bool> DoComfortZoneAction(CraftActions action)
        {
            if(!HasComfortZoneAura) await DoAction(CraftActions.ComfortZone);
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
        
        private int FocusedSynthesisProcess = 0;

        private const int FocusedSynthesisCp = 12;
        
        public override async Task<bool> OnStart()
        {
            await Coroutine.Sleep(200);
            FocusedSynthesisProcess = 0;
            return true;
        }
        
        public override async Task<bool> DoExecute()
        {
            FocusedSynthesisProcess = int.Parse(param);
			
            await Cast(CraftActions.MakersMark); //坚实的心得
            
            await DoAction(CraftActions.ComfortZone);     //安逸
            await DoAction(CraftActions.InnerQuiet);      //内静
            
            await DoAction(CraftActions.SteadyHand); //稳手
            await DoAction(CraftActions.PiecebyPiece);//渐进
            await DoAction(CraftActions.PiecebyPiece);//渐进
            
            // 获得坚实的心得剩余次数
            uint left = MakersMarkNums;
            
            for (int i = 0;i< left;i++)
            {
                await DoFlawlessSynthesis();
            }

            // 判断需要多少次注视制作
            long FocusedSynthesisTimes = LeftProcess / FocusedSynthesisProcess;
            FocusedSynthesisTimes += LeftProcess % FocusedSynthesisProcess == 0 ? 0 : 1;

            Logger.Info("还剩余{0}进度，还需要推{1}次注视制作", LeftProcess, FocusedSynthesisTimes);

            // 加工循环
            await DoComfortZoneAction(CraftActions.ManipulationII);    // 掌握II
            await DoComfortZoneAction(CraftActions.SteadyHandII);  //稳手II

            for(int i = 0; i < 5; i++)
            {
                await Cast(CraftActions.PrudentTouch);  // 简约加工
            }

            // 必须保留的CP数，注视制作，比尔格的祝福，新颖II，阔步，改革，稳手
            long endCp = 12 * FocusedSynthesisTimes + 24 + 32 + 32 + 18 + 22;

            // 如果剩余CP能够用新颖/新颖II（保留一次注视加工的CP数）
            long leftCp = CurrentCP + ComfortZoneNums * 8 - endCp - 21*4 - 25;

            if(leftCp >= 32)
            {
                await DoComfortZoneAction(CraftActions.IngenuityII);
            } else if(leftCp >= 24 && HasAction(CraftActions.Ingenuity))
            {
                // 如果有新颖
                await DoComfortZoneAction(CraftActions.Ingenuity);
            }

            if (HasIngenuity || HasIngenuityII)
                await Cast(CraftActions.SteadyHandII);  // 稳手II
            else
                await DoComfortZoneAction(CraftActions.SteadyHandII);   // 如果没有新颖BUFF，可以用秘诀和安逸

            for (int i = 0; i < 4; i++)
            {
                await Cast(CraftActions.PrudentTouch);  // 简约加工
            }
            
            
            leftCp = CurrentCP + ComfortZoneNums * 8 - endCp;

            // 如果剩余CP能推两次简约加工并且把稳手换成稳手II
            if (leftCp - 3 - 21 * 2 >= 0)
            {   
                // 如果剩余CP数够两次简约加工
                await Cast(CraftActions.PrudentTouch);

                // 判断是否可以多推一次注视加工，最多两次
                long touchTimes = (leftCp -3 - 21 * 2 ) / 25;
                touchTimes = touchTimes > 2 ? 2 : touchTimes;

                for(int i=0;i<touchTimes;i++)
                {
                    await Cast(CraftActions.Observe);
                    await Cast(CraftActions.FocusedTouch);
                }
                
                await Cast(CraftActions.SteadyHandII);
                await Cast(CraftActions.PrudentTouch);
                await Cast(CraftActions.Innovation);
            }
            else
            {
                // 如果剩余CP数只够一次注视加工
                if (leftCp - 25 >= 25)
                {
                    await Cast(CraftActions.Observe);
                    await Cast(CraftActions.FocusedTouch);
                }

                await Cast(CraftActions.Observe);
                await Cast(CraftActions.FocusedTouch);

                await Cast(CraftActions.SteadyHand);
                await Cast(CraftActions.Innovation);
            }

            await Cast(CraftActions.GreatStrides);

            if (ConditionExcellent)
            {
                await Cast(CraftActions.ByregotsBlessing);
                await Cast(CraftActions.IngenuityII);
            }
            else
            {
                await Cast(CraftActions.IngenuityII);
                await Cast(CraftActions.ByregotsBlessing);
            }

            for (long i = 0; i <= FocusedSynthesisTimes; i++)
            {
                await Cast(CraftActions.Observe);
                await Cast(CraftActions.FocusedSynthesis);
            }

            while(IsCrafting)
            {
                await Cast(CraftActions.CarefulSynthesisII);
            }

            return true;
        }
        

    }
}