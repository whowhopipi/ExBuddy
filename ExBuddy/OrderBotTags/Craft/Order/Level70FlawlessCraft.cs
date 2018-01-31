namespace ExBuddy.OrderBotTags.Craft.Order
{
    using Helpers;
    using System.Threading.Tasks;
    using ff14bot.Managers;
    using ff14bot.Enums;
    using Behaviors.Objects;
    using ff14bot;
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
                && await checkSkill(CraftActions.WasteNotII,"缺少技能：简约2")
                && await checkSkill(CraftActions.PreciseTouch,"缺少技能：集中加工")
                && await checkSkill(CraftActions.HastyTouch,"缺少技能：仓促")
                && await checkSkill(CraftActions.BasicTouch,"缺少技能：加工")
                && await checkSkill(CraftActions.TricksoftheTrade,"缺少技能：秘诀")
                && await checkSkill(CraftActions.MastersMendII,"缺少技能：精修2")
                && await checkSkill(CraftActions.ByregotsBlessing,"缺少技能：比尔格的祝福")
                && await checkSkill(CraftActions.GreatStrides,"缺少技能：阔步")
                && await checkSkill(CraftActions.CarefulSynthesisII, "缺少技能：模范制作II");
        }
        
        private bool HasInnovation
        {
            get
            {
                return HasAction(CraftActions.Innovation);
            }
        }

        private bool NeedTricksoft
        {
            get
            {
                return Core.Me.CurrentCP < Core.Me.MaxCP && IsGoodCondition();
            }
        }

        private async Task<bool> CheckAction(CraftActions ability)
        {
            if (NeedTricksoft && hightCount < processCount)
            {
                int leftCp = Core.Me.MaxCP - Core.Me.CurrentCP;
                await Cast(CraftActions.TricksoftheTrade);//秘诀
                if(leftCp >= 18)
                    hightCount++;
            }
            await Cast(ability);
            if (NeedTricksoft && hightCount < processCount)
            {
                int leftCp = Core.Me.MaxCP - Core.Me.CurrentCP;
                await Cast(CraftActions.TricksoftheTrade);//秘诀
                if(leftCp >= 18)
                    hightCount++;
            }

            return true;
        }

        private async Task<bool> CheckMakersMarkAction(CraftActions ability)
        {
            // 获得坚实的心得的次数
            uint left = Core.Me.GetAuraById(878).Value;

            if (IsGoodCondition() && left > needSuccessFlaw + 1)
            {
                await Cast(CraftActions.TricksoftheTrade);//秘诀
                hightCount++;
            }
            await Cast(ability);
            if (IsGoodCondition())
            {
                await Cast(CraftActions.TricksoftheTrade);//秘诀
                hightCount++;
            }

            return true;
        }

        private async Task<bool> DoFlawlessSynthesis(int index,uint left)
        {
            int oldProgress = CraftingManager.Progress;

            if (NeedTricksoft && IsGoodCondition() && (needSuccessFlaw == successFlaw || (left - index - 1 > needSuccessFlaw - successFlaw)))
            {
                await Cast(CraftActions.TricksoftheTrade);   //秘诀

                hightCount++;
            } else
            {
                await Cast(CraftActions.FlawlessSynthesis);
            }

            // 判断坚实制作是否成功
            int newProgress = CraftingManager.Progress;
            if(newProgress > oldProgress)
            {
                successFlaw++;
            }

            return true;
        }

        private async Task<bool> DoProcess()
        {
            if (IsGoodCondition())
            {
                if(hightCount > 0)
                {
                    await Cast(CraftActions.PreciseTouch);   // 集中加工
                    hightCount--;
                }
                else
                {
                    await Cast(CraftActions.HastyTouch);     // 仓促
                }
            } else
            {
                if(hightCount >= processCount)
                {
                    await Cast(CraftActions.BasicTouch);     //加工
                    hightCount--;
                }
                else
                {
                    await Cast(CraftActions.HastyTouch);     //仓促
                }
            }

            processCount--;

            return true;
        }
        
        private int needSuccessFlaw = 7;

        private int processCount = 0;
        private int hightCount = 0;

        private int successFlaw = 0;    //坚实制作成功数

        public override async Task<bool> OnStart()
        {
            successFlaw = 0;
            processCount = 11;

            hightCount = (Core.Me.MaxCP - 455) / 18;
            if(!HasInnovation)
            {
                hightCount++;
            }
            return true;
        }
        
        public override async Task<bool> DoExecute()
        {
            int oneProgress = int.Parse(param);

            await Cast(CraftActions.MakersMark); //坚实的心得
            
            await CheckMakersMarkAction(CraftActions.ComfortZone);     //安逸
            await CheckMakersMarkAction(CraftActions.SteadyHand); //稳手
            await CheckMakersMarkAction(CraftActions.PiecebyPiece);//渐进
            await CheckMakersMarkAction(CraftActions.PiecebyPiece);//渐进

            // 获得剩余工数
            int leftProgress = CraftingManager.ProgressRequired - CraftingManager.Progress;

            // 获得坚实的心得剩余次数
            uint left = Core.Me.GetAuraById(878).Value;

            int minMark = (leftProgress - oneProgress * 3) / 40;
            minMark += (leftProgress - oneProgress * 3) % 40 == 0 ? 0 : 1;
            needSuccessFlaw = minMark;

            Logger.Info("最低成功坚实数：{0}", needSuccessFlaw);

            for (int i = 0;i< left;i++)
            {
                await DoFlawlessSynthesis(i + 1,left);
            }

            // 判断加工次数
            if (successFlaw < needSuccessFlaw)
            {
                processCount -= (needSuccessFlaw - successFlaw) / 2;
            }

            await CheckAction(CraftActions.ComfortZone);     //安逸
            await CheckAction(CraftActions.InnerQuiet);      //内静
            await Cast(CraftActions.WasteNotII);          //简约2
            await Cast(CraftActions.SteadyHandII);        //稳手2

            for(int i = 0; i < 5; i++)
            {
                await DoProcess();
            }

            // 判断一次模范制作2能够推进多少进度
            int start = CraftingManager.Progress;
            await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
            int end = CraftingManager.Progress;

            int step = end - start;

            int leftProcess = CraftingManager.ProgressRequired - end;

            // 计算还差几次模范2可以推满

            int leftCount = leftProcess / step;
            leftCount += leftProcess % step == 0 ? 0 : 1;
            Logger.Verbose("还需要进行{0}次模范制作2才能推满", leftCount);

            if(leftCount > 1)
            {
                await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
                leftCount--;
            } else
            {
                if (IsGoodCondition())
                {
                    await Cast(CraftActions.TricksoftheTrade);//秘诀
                    hightCount++;
                }
                else
                {
                    await Cast(CraftActions.HastyTouch); // 仓促
                }
            }

            await CheckAction(CraftActions.ComfortZone);     //安逸
            await Cast(CraftActions.SteadyHandII);        //稳手2
            await DoProcess();
            await Cast(CraftActions.MastersMendII);    //精修2
            await DoProcess();
            await DoProcess();
            await DoProcess();

            await CheckAction(CraftActions.SteadyHandII);     //稳手2
            Logger.Verbose("还可以进行{0}次加工", 3-leftCount);
            switch(3-leftCount)
            {
                case 2:
                    await DoProcess();
                    await DoProcess();
                    break;
                case 1:
                    await DoProcess();
                    await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
                    break;
                case 0:
                    await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
                    await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
                    break;
            }
            
            //阔步、改革、比格
            if(CraftingManager.Condition == CraftingCondition.Excellent)
            {
                await Cast(CraftActions.ByregotsBlessing);       //比尔格的祝福
            } else
            {
                await Cast(CraftActions.GreatStrides);       //阔步
                if (IsGoodCondition())
                {
                    await Cast(CraftActions.ByregotsBlessing);       //比尔格的祝福
                }
                else
                {
                    if(HasInnovation && Core.Me.CurrentCP > 42)
                        await Cast(CraftActions.Innovation);     //改革
                    await Cast(CraftActions.ByregotsBlessing);       //比尔格的祝福
                }
            }

            while (IsCrafting())
            {
                await Cast(CraftActions.CarefulSynthesisII);  //模范制作2
            }

            return true;
        }

    }
}