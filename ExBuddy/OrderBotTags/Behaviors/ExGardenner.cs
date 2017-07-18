namespace ExBuddy.OrderBotTags.Behaviors
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Clio.XmlEngine;
    using Buddy.Coroutines;

    using ff14bot;
    using ff14bot.Managers;
    using ff14bot.Enums;
    using ff14bot.Helpers;
    using ff14bot.RemoteWindows;

    [XmlElement("ExGardenner")]
    [XmlElement("Gardenner")]
    public class ExGardenner : ExProfileBehavior
    {
        [XmlAttribute("AlwaysWater")]
        public bool AlwaysWater { get; set; }

        private const int PostInteractDelay = 2300;

        protected override async Task<bool> Main()
        {
            var watering = GardenManager.Plants.Where(r => !Blacklist.Contains(r) && r.Distance2D(Core.Player) < 5).ToArray();
            foreach (var plant in watering)
            {
                //Water it if it needs it or if we have fertilized it 5 or more times.
                if (AlwaysWater || GardenManager.NeedsWatering(plant))
                {
                    var result = GardenManager.GetCrop(plant);
                    if (result != null)
                    {
                        Log("Watering {0} {1:X}", result.CurrentLocaleName, plant.ObjectId);
                        plant.Interact();
                        if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                        {
                            Talk.Next();
                            if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
                            {
                                if (await Coroutine.Wait(5000, () => SelectString.LineCount > 0))
                                {
                                    //Harvest drops it down to two
                                    if (SelectString.LineCount == 4)
                                    {
                                        SelectString.ClickSlot(1);
                                        await Coroutine.Sleep(PostInteractDelay);
                                    }
                                    else
                                    {
                                        SelectString.ClickSlot((uint)(SelectString.LineCount - 1));
                                        Blacklist.Add(plant, BlacklistFlags.All, TimeSpan.FromMinutes(61), "Plant is ready to be harvested");
                                    }
                                }
                                await Coroutine.Wait(3000, () => !SelectString.IsOpen);
                            }
                        }
                    }
                    else
                    {
                        Log("GardenManager.GetCrop returned null {0:X}", plant.ObjectId);
                    }
                }
            }

            var plants = GardenManager.Plants.Where(r => r.Distance2D(Core.Player) < 5).ToArray();
            foreach (var plant in plants)
            {
                var result = GardenManager.GetCrop(plant);
                if (result != null)
                {
                    Log("Fertilizing {0} {1:X}", result.CurrentLocaleName, plant.ObjectId);
                    plant.Interact();

                    if (await Coroutine.Wait(5000, () => Talk.DialogOpen))
                    {
                        Talk.Next();
                        if (await Coroutine.Wait(5000, () => SelectString.IsOpen))
                        {
                            if (await Coroutine.Wait(5000, () => SelectString.LineCount > 0))
                            {
                                //Harvest drops it down to two
                                if (SelectString.LineCount == 4)
                                {
                                    SelectString.ClickSlot(0);
                                    if (await Coroutine.Wait(2000, () => GardenManager.ReadyToFertilize))
                                    {
                                        if (GardenManager.Fertilize() == FertilizeResult.Success)
                                        {
                                            LogVerbose("Plant with objectId {0:X} was fertilized", plant.ObjectId);
                                            await Coroutine.Sleep(PostInteractDelay);
                                        }
                                    }
                                    else
                                    {
                                        LogVerbose("Plant with objectId {0:X} not able to be fertilized, trying again later", plant.ObjectId);
                                    }
                                }
                                else
                                {
                                    SelectString.ClickSlot((uint)(SelectString.LineCount - 1));
                                    Blacklist.Add(plant, BlacklistFlags.All, TimeSpan.FromMinutes(61), "Plant is ready to be harvested");
                                }
                            }
                            await Coroutine.Wait(3000, () => !SelectString.IsOpen);
                        }
                    }
                }
            }
			return isDone = true;
        }
    }
}
