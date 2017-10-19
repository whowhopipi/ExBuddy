namespace ExBuddy.OrderBotTags.Behaviors.Objects
{
    using Clio.Utilities;
    using ExBuddy.GameObjects.Npcs;
    using ExBuddy.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Data
    {
        public static readonly Dictionary<Locations, IList<INpc>> NpcMap = new Dictionary<Locations, IList<INpc>>
        {
            {
                Locations.RhalgrsReach,
                new INpc[]
                {
                    new MasterPieceSupply
                    {
                        AetheryteId = 104,
                        ZoneId = 635,
                        Location = new Vector3("-67.64739, 0.00999999, 63.51791"),
                        NpcId = 1019457
                    },
                    new ShopExchangeCurrency
                    {
                        AetheryteId = 104,
                        ZoneId = 635,
                        Location = new Vector3("-67.64739, 0.00999999, 63.51791"),
                        NpcId = 1019458
                    }
                }
            },
            {
                Locations.Idyllshire,
                new INpc[]
                {
                    new MasterPieceSupply
                    {
                        AetheryteId = 75,
                        ZoneId = 478,
                        Location = new Vector3("-18.29561, 206.5211, 45.12088"),
                        NpcId = 1012300
                    },
                    new ShopExchangeCurrency
                    {
                        AetheryteId = 75,
                        ZoneId = 478,
                        Location = new Vector3("-20.6455, 206.5211, 47.25714"),
                        NpcId = 1012301
                    }
                }
            },
            {
                Locations.MorDhona,
                new INpc[]
                {
                    new MasterPieceSupply
                    {
                        AetheryteId = 24,
                        ZoneId = 156,
                        Location = new Vector3("50.33948, 31.13618, -737.4532"),
                        NpcId = 1013396
                    },
                    new ShopExchangeCurrency
                    {
                        AetheryteId = 24,
                        ZoneId = 156,
                        Location = new Vector3("47.34875, 31.15659, -737.4838"),
                        NpcId = 1013397
                    }
                }
            },
            {
                Locations.UldahStepsOfNald,
                new INpc[]
                {
                    new FreeCompanyChest
                    {
                        AetheryteId = 9,
                        ZoneId = 130,
                        Location = new Vector3("-149.3096, 4.53186, -91.38635"),
                        NpcId = 2000470,
                        Name = "Company Chest"
                    }
                }
            },
            {
                Locations.LimsaLominsaLowerDecks,
                new INpc[]
                {
                    new FreeCompanyChest
                    {
                        AetheryteId = 8,
                        ZoneId = 129,
                        Location = new Vector3("-200, 17.04425, 58.76245"),
                        NpcId = 2000470,
                        Name = "Company Chest"
                    }
                }
            }
        };

        public static readonly Dictionary<ShopItem, ShopItemInfo> ShopItemMap = new Dictionary<ShopItem, ShopItemInfo>
        {
#if RB_CN
            #region RedCrafter50

            {
                ShopItem.SoulOfTheCrafter,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SoulOfTheCrafter,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 10336,
                    Cost = 480,
                    Yield = 1
                }
            },
            {
                ShopItem.CommercialEngineeringManual,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommercialEngineeringManual,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12667,
                    Cost = 30,
                    Yield = 1
                }
            },
            {
                ShopItem.CrpDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrpDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12659,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.BsmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BsmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12660,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.ArmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ArmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12661,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.GsmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GsmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12662,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.LtwDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LtwDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12663,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.WvrDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.WvrDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12664,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.AlcDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AlcDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12665,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.CulDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CulDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12666,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.CompetenceIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CompetenceIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5702,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CompetenceV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CompetenceV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5703,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.CunningIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CunningIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5707,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CunningV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CunningV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5708,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.CommandIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommandIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5712,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CommandV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommandV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5713,
                    Cost = 400,
                    Yield = 1
                }
            },

            #endregion RedCrafter50

            #region RedCrafter58

            {
                ShopItem.BlueCrafterToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueCrafterToken - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12839,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GoblinCup,
                new ShopItemInfo
                {
                    Index = 100 + (int) ShopItem.GoblinCup - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14104,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.SweetCreamMilk,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SweetCreamMilk - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16734,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.SweetCreamMilkHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SweetCreamMilkHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16734,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.StoneCheese,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneCheese - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16735,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.StoneCheeseHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneCheeseHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16735,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavensEgg,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavensEgg - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 15652,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavensEggHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavensEggHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 15652,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.CarbonFiber,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CarbonFiber - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 5339,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CarbonFiberHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CarbonFiberHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 5339,
                    Cost = 62,
                    Yield = 1
                }
            },
            {
                ShopItem.LoaghtanFilet,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LoaghtanFilet - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14145,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.LoaghtanFiletHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LoaghtanFiletHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14145,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldenApple,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldenApple - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14142,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldenAppleHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldenAppleHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14142,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.SolsticeGarlic,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SolsticeGarlic - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14143,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.SolsticeGarlicHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SolsticeGarlicHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14143,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.MatureOliveOil,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MatureOliveOil - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14144,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.MatureOliveOilHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MatureOliveOilHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14144,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.PowderedMermanHorn,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PowderedMermanHorn - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14937,
                    Cost = 6,
                    Yield = 1
                }
            },
            {
                ShopItem.PowderedMermanHornHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PowderedMermanHornHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14937,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.BouillonCube,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BouillonCube - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12905,
                    Cost = 4,
                    Yield = 1
                }
            },
            {
                ShopItem.BouillonCubeHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BouillonCubeHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12905,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalSoySauce,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalSoySauce - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12906,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalSoySauceHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalSoySauceHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12906,
                    Cost = 37,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalMisoPaste,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalMisoPaste - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12907,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalMisoPasteHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalMisoPasteHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12907,
                    Cost = 37,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavenlyKukuruPowder,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavenlyKukuruPowder - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12886,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavenlyKukuruPowderHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavenlyKukuruPowderHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12886,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsHat,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsHat - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11958,
                    Cost = 60,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsGown,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsGown - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11963,
                    Cost = 135,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsGloves,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsGloves - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11968,
                    Cost = 60,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsHose,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsHose - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11976,
                    Cost = 52,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsThighboots,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsThighboots - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11981,
                    Cost = 52,
                    Yield = 1
                }
            },

            #endregion RedCrafter58

            #region RedCrafter61

            {
                ShopItem.DomanIronHalfheartSaw,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHalfheartSaw - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19527,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronClawHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronClawHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19538,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronLumpHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronLumpHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19528,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronFile,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronFile - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19539,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronRaisingHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronRaisingHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19529,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronPliers,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronPliers - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19540,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DuriumTextureHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DuriumTextureHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19530,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.SlateGrindingWheel,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SlateGrindingWheel - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19541,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronHeadKnife,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHeadKnife - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19531,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronAwl,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronAwl - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19542,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DzoHornNeedle,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DzoHornNeedle - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19532,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.PineSpinningWheel,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PineSpinningWheel - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19543,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronAlembic,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronAlembic - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19533,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronMortar,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronMortar - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19544,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronFrypan,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronFrypan - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19534,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronCulinaryKnife,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronCulinaryKnife - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19545,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCapofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCapofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19632,
                    Cost = 180,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuRobeofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuRobeofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19633,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DuriumChaplets,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DuriumChaplets - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19634,
                    Cost = 180,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCulottesofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCulottesofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19635,
                    Cost = 160,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinBootsofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinBootsofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19636,
                    Cost = 160,
                    Yield = 1
                }
            },

            #endregion RedCrafter61

            #region YellowCrafterItems

            {
                ShopItem.KoshuPork,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KoshuPork - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 19876,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.TeaBrick,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TeaBrick - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 19840,
                    Cost = 15,
                    Yield = 1
                }
            },

            #endregion YellowCrafterItems

            #region RedGatherer50

            {
                ShopItem.HiCordial,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HiCordial - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12669,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.CommercialSurvivalManual,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommercialSurvivalManual - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12668,
                    Cost = 30,
                    Yield = 1
                }
            },
            {
                ShopItem.GiantCraneFly,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GiantCraneFly - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12712,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.BruteLeech,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BruteLeech - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12711,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.FiendWorm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.FiendWorm - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12710,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.MagmaWorm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MagmaWorm - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12709,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.RedBalloon,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RedBalloon - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12708,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.GuerdonIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuerdonIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5687,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GuerdonV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuerdonV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5688,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.GuileIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuileIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5692,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GuileV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuileV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5693,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.GraspIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GraspIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5697,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GraspV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GraspV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5698,
                    Cost = 400,
                    Yield = 1
                }
            },

            #endregion RedGatherer50

            #region RedGatherer58

            {
                ShopItem.BlueGatherToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueGatherToken - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12841,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GoblinDice,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoblinDice - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 14105,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersScarf,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersScarf - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11986,
                    Cost = 100,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersVest,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersVest - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11991,
                    Cost = 135,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersWristguards,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersWristguards - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11996,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersSlops,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersSlops - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12004,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersShoes,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersShoes - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12009,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.CrownTrout,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrownTrout - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13737,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.CrownTroutHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrownTroutHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13737,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.RetributionStaff,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RetributionStaff - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13738,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.RetributionStaffHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RetributionStaffHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13738,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.ThiefBetta,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ThiefBetta - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13739,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.ThiefBettaHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ThiefBettaHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13739,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldsmithCrab,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldsmithCrab - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13740,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldsmithCrabHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldsmithCrabHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13740,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.Pterodactyl,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Pterodactyl - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13733,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.PterodactylHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PterodactylHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13733,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.Eurhinosaur,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Eurhinosaur - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13734,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.EurhinosaurHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.EurhinosaurHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13734,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GemMarimo,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GemMarimo - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13736,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.GemMarimoHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GemMarimoHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13736,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.Sphalerite,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Sphalerite - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13750,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.SphaleriteHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SphaleriteHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13750,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.WindSilk,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.WindSilk - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13744,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.CloudCottonBoll,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CloudCottonBoll - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13753,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.CloudCottonBollHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CloudCottonBollHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13753,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.DinosaurLeather,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DinosaurLeather - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13745,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.RoyalMistletoe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RoyalMistletoe - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13752,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.RoyalMistletoeHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RoyalMistletoeHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13752,
                    Cost = 25,
                    Yield = 1
                }
            },

            #endregion RedGatherer58

            #region RedGatherer61

            {
                ShopItem.FolkloreGatherToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.FolkloreGatherToken - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20260,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronPickaxe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronPickaxe - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19535,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronSledgehammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronSledgehammer - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19546,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronHatchet,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHatchet - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19536,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronScythe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronScythe - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19547,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.PineFishingRod,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PineFishingRod - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19537,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinCapofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinCapofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19637,
                    Cost = 300,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCoatofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCoatofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19638,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinFingerlessGlovesofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinFingerlessGlovesofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19639,
                    Cost = 200,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCulottesofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCulottesofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19640,
                    Cost = 150,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinBootsofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinBootsofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19641,
                    Cost = 150,
                    Yield = 1
                }
            },
            {
                ShopItem.SuspendingMinnow,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SuspendingMinnow - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20619,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.BreamLure,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BreamLure - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20618,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.Silkworm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Silkworm - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20616,
                    Cost = 5,
                    Yield = 1
                }
            },

            #endregion RedGatherer61

            #region YellowGathererItems

            {
                ShopItem.BlueBobbit,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueBobbit - 700,
                    ShopType = ShopType.YellowGathererItems,
                    ItemId = 20676,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.StoneflyLarva,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneflyLarva - 700,
                    ShopType = ShopType.YellowGathererItems,
                    ItemId = 20675,
                    Cost = 5,
                    Yield = 1
                }
            }

            #endregion YellowGathererItems
#else
            #region RedCrafter50

            {
                ShopItem.SoulOfTheCrafter,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SoulOfTheCrafter,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 10336,
                    Cost = 480,
                    Yield = 1
                }
            },
            {
                ShopItem.CommercialEngineeringManual,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommercialEngineeringManual,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12667,
                    Cost = 30,
                    Yield = 1
                }
            },
            {
                ShopItem.CrpDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrpDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12659,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.BsmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BsmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12660,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.ArmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ArmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12661,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.GsmDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GsmDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12662,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.LtwDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LtwDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12663,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.WvrDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.WvrDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12664,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.AlcDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AlcDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12665,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.CulDelineation,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CulDelineation,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 12666,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.CompetenceIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CompetenceIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5702,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CompetenceV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CompetenceV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5703,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.CunningIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CunningIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5707,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CunningV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CunningV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5708,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.CommandIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommandIV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5712,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CommandV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommandV,
                    ShopType = ShopType.RedCrafter50,
                    ItemId = 5713,
                    Cost = 400,
                    Yield = 1
                }
            },

            #endregion RedCrafter50

            #region RedCrafter58

            {
                ShopItem.BlueCrafterToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueCrafterToken - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12839,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GoblinCup,
                new ShopItemInfo
                {
                    Index = 100 + (int) ShopItem.GoblinCup - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14104,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.SweetCreamMilk,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SweetCreamMilk - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16734,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.SweetCreamMilkHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SweetCreamMilkHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16734,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.StoneCheese,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneCheese - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16735,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.StoneCheeseHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneCheeseHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 16735,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavensEgg,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavensEgg - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 15652,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavensEggHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavensEggHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 15652,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.CarbonFiber,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CarbonFiber - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 5339,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.CarbonFiberHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CarbonFiberHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 5339,
                    Cost = 62,
                    Yield = 1
                }
            },
            {
                ShopItem.LoaghtanFilet,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LoaghtanFilet - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14145,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.LoaghtanFiletHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.LoaghtanFiletHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14145,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldenApple,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldenApple - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14142,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldenAppleHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldenAppleHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14142,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.SolsticeGarlic,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SolsticeGarlic - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14143,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.SolsticeGarlicHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SolsticeGarlicHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14143,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.MatureOliveOil,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MatureOliveOil - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14144,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.MatureOliveOilHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MatureOliveOilHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14144,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.PowderedMermanHorn,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PowderedMermanHorn - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14937,
                    Cost = 6,
                    Yield = 1
                }
            },
            {
                ShopItem.PowderedMermanHornHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PowderedMermanHornHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 14937,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.BouillonCube,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BouillonCube - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12905,
                    Cost = 4,
                    Yield = 1
                }
            },
            {
                ShopItem.BouillonCubeHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BouillonCubeHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12905,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalSoySauce,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalSoySauce - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12906,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalSoySauceHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalSoySauceHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12906,
                    Cost = 37,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalMisoPaste,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalMisoPaste - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12907,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.OrientalMisoPasteHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.OrientalMisoPasteHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12907,
                    Cost = 37,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavenlyKukuruPowder,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavenlyKukuruPowder - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12886,
                    Cost = 8,
                    Yield = 1
                }
            },
            {
                ShopItem.HeavenlyKukuruPowderHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HeavenlyKukuruPowderHq - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 12886,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsHat,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsHat - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11958,
                    Cost = 60,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsGown,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsGown - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11963,
                    Cost = 135,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsGloves,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsGloves - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11968,
                    Cost = 60,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsHose,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsHose - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11976,
                    Cost = 52,
                    Yield = 1
                }
            },
            {
                ShopItem.AdeptsThighboots,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.AdeptsThighboots - 100,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 11981,
                    Cost = 52,
                    Yield = 1
                }
            },

            #endregion RedCrafter58

            #region RedCrafter61

            {
                ShopItem.DomanIronHalfheartSaw,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHalfheartSaw - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19527,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronClawHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronClawHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19538,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronLumpHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronLumpHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19528,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronFile,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronFile - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19539,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronRaisingHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronRaisingHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19529,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronPliers,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronPliers - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19540,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DuriumTextureHammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DuriumTextureHammer - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19530,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.SlateGrindingWheel,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SlateGrindingWheel - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19541,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronHeadKnife,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHeadKnife - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19531,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronAwl,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronAwl - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19542,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DzoHornNeedle,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DzoHornNeedle - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19532,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.PineSpinningWheel,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PineSpinningWheel - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19543,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronAlembic,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronAlembic - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19533,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronMortar,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronMortar - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19544,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronFrypan,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronFrypan - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19534,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronCulinaryKnife,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronCulinaryKnife - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19545,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCapofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCapofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19632,
                    Cost = 180,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuRobeofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuRobeofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19633,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DuriumChaplets,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DuriumChaplets - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19634,
                    Cost = 180,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCulottesofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCulottesofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19635,
                    Cost = 160,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinBootsofCrafting,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinBootsofCrafting - 200,
                    ShopType = ShopType.RedCrafter61,
                    ItemId = 19636,
                    Cost = 160,
                    Yield = 1
                }
            },

            #endregion RedCrafter61

            #region YellowCrafterItems

            {
                ShopItem.KoshuPork,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KoshuPork - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 19876,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.TeaBrick,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TeaBrick - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 19840,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.CrimsonPepper,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrimsonPepper - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 21301,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.SecretRecipeBroth,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SecretRecipeBroth - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 21089,
                    Cost = 15,
                    Yield = 1
                }
            },
            {
                ShopItem.GyrAbanianAlchemic,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GyrAbanianAlchemic - 300,
                    ShopType = ShopType.YellowCrafterItems,
                    ItemId = 21082,
                    Cost = 250,
                    Yield = 1
                }
            },

            #endregion YellowCrafterItems

            #region RedGatherer50

            {
                ShopItem.HiCordial,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.HiCordial - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12669,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.CommercialSurvivalManual,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CommercialSurvivalManual - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12668,
                    Cost = 30,
                    Yield = 1
                }
            },
            {
                ShopItem.GiantCraneFly,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GiantCraneFly - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12712,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.BruteLeech,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BruteLeech - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12711,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.FiendWorm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.FiendWorm - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12710,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.MagmaWorm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.MagmaWorm - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12709,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.RedBalloon,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RedBalloon - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 12708,
                    Cost = 1,
                    Yield = 1
                }
            },
            {
                ShopItem.GuerdonIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuerdonIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5687,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GuerdonV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuerdonV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5688,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.GuileIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuileIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5692,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GuileV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GuileV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5693,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.GraspIV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GraspIV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5697,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GraspV,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GraspV - 400,
                    ShopType = ShopType.RedGatherer50,
                    ItemId = 5698,
                    Cost = 400,
                    Yield = 1
                }
            },

            #endregion RedGatherer50

            #region RedGatherer58

            {
                ShopItem.BlueGatherToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueGatherToken - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12841,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.GoblinDice,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoblinDice - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 14105,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersScarf,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersScarf - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11986,
                    Cost = 100,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersVest,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersVest - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11991,
                    Cost = 135,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersWristguards,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersWristguards - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 11996,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersSlops,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersSlops - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12004,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.TrailblazersShoes,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TrailblazersShoes - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 12009,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.CrownTrout,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrownTrout - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13737,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.CrownTroutHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CrownTroutHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13737,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.RetributionStaff,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RetributionStaff - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13738,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.RetributionStaffHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RetributionStaffHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13738,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.ThiefBetta,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ThiefBetta - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13739,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.ThiefBettaHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.ThiefBettaHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13739,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldsmithCrab,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldsmithCrab - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13740,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.GoldsmithCrabHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GoldsmithCrabHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13740,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.Pterodactyl,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Pterodactyl - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13733,
                    Cost = 20,
                    Yield = 1
                }
            },
            {
                ShopItem.PterodactylHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PterodactylHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13733,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.Eurhinosaur,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Eurhinosaur - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13734,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.EurhinosaurHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.EurhinosaurHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13734,
                    Cost = 12,
                    Yield = 1
                }
            },
            {
                ShopItem.GemMarimo,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GemMarimo - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13736,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.GemMarimoHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.GemMarimoHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13736,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.Sphalerite,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Sphalerite - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13750,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.SphaleriteHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SphaleriteHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13750,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.WindSilk,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.WindSilk - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13744,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.CloudCottonBoll,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CloudCottonBoll - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13753,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.CloudCottonBollHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.CloudCottonBollHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13753,
                    Cost = 25,
                    Yield = 1
                }
            },
            {
                ShopItem.DinosaurLeather,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DinosaurLeather - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13745,
                    Cost = 75,
                    Yield = 1
                }
            },
            {
                ShopItem.RoyalMistletoe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RoyalMistletoe - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13752,
                    Cost = 10,
                    Yield = 1
                }
            },
            {
                ShopItem.RoyalMistletoeHq,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.RoyalMistletoeHq - 500,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 13752,
                    Cost = 25,
                    Yield = 1
                }
            },

            #endregion RedGatherer58

            #region RedGatherer61

            {
                ShopItem.FolkloreGatherToken,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.FolkloreGatherToken - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20260,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronPickaxe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronPickaxe - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19535,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronSledgehammer,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronSledgehammer - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19546,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronHatchet,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronHatchet - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19536,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.DomanIronScythe,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.DomanIronScythe - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19547,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.PineFishingRod,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.PineFishingRod - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19537,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinCapofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinCapofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19637,
                    Cost = 300,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCoatofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCoatofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19638,
                    Cost = 400,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinFingerlessGlovesofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinFingerlessGlovesofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19639,
                    Cost = 200,
                    Yield = 1
                }
            },
            {
                ShopItem.KudzuCulottesofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.KudzuCulottesofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19640,
                    Cost = 150,
                    Yield = 1
                }
            },
            {
                ShopItem.TigerskinBootsofGathering,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.TigerskinBootsofGathering - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 19641,
                    Cost = 150,
                    Yield = 1
                }
            },
            {
                ShopItem.Silkworm,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.Silkworm - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20616,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.BreamLure,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BreamLure - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20618,
                    Cost = 50,
                    Yield = 1
                }
            },
            {
                ShopItem.SuspendingMinnow,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.SuspendingMinnow - 600,
                    ShopType = ShopType.RedGatherer61,
                    ItemId = 20619,
                    Cost = 50,
                    Yield = 1
                }
            },

            #endregion RedGatherer61

            #region YellowGathererItems
            
            {
                ShopItem.StoneflyLarva,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.StoneflyLarva - 700,
                    ShopType = ShopType.YellowGathererItems,
                    ItemId = 20675,
                    Cost = 5,
                    Yield = 1
                }
            },
            {
                ShopItem.BlueBobbit,
                new ShopItemInfo
                {
                    Index = (int) ShopItem.BlueBobbit - 700,
                    ShopType = ShopType.YellowGathererItems,
                    ItemId = 20676,
                    Cost = 5,
                    Yield = 1
                }
            },

            #endregion YellowGathererItems
#endif
        };

        public static IEnumerable<INpc> GetNpcsByLocation(Locations location)
        {
            IList<INpc> npcs;
            if (NpcMap.TryGetValue(location, out npcs))
                return npcs;

            return Enumerable.Empty<INpc>();
        }

        public static IEnumerable<T> GetNpcsByLocation<T>(Locations location) where T : INpc
        {
            return GetNpcsByLocation(location).OfType<T>();
        }
    }
}