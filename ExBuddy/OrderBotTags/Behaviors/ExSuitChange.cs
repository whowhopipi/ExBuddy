using Buddy.Coroutines;
using Clio.XmlEngine;
using ExBuddy.Attributes;
using ExBuddy.Helpers;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [LoggerName("Suit")]
    [XmlElement("Suit")]
    public class ExSuitChange : ExProfileBehavior
    {

        [XmlAttribute("SuitId")]
        public int SuitId { get; set; }

        [DefaultValue("")]
        [XmlAttribute("SuitName")]
        public string SuitName { get; set; }

        [DefaultValue(true)]
        [XmlAttribute("Repair")]
        public bool RepairFlag { set; get; }
    
        protected override async Task<bool> Main()
        {
            StatusText = string.Format("更换套装{0}:{1}", SuitId, SuitName);

            Logger.Info("准备更换套装{0},{1}",SuitId, SuitName);

            // TODO 根据是否能够传送来判断当前是否能够更换套装，如果有更好的判断方式，则修改
            int count = 0;
            bool flag = false;

            while(!flag && count < 5)
            {
                flag = await Coroutine.Wait(2000, () => WorldManager.CanTeleport());
                count++;
            }
            
            if(!flag)
            {
                Logger.Warn("当前状态不适合更换套装");
                isDone = true;
                return false;
            }

            // await Coroutine.Sleep(2000);
            ChatManager.SendChat("/gs change "　+ SuitId);
            await Coroutine.Sleep(2000);

            if (Core.Player.CurrentJob == ff14bot.Enums.ClassJobType.Botanist && !Core.Player.HasAura(221))
            {
                 if(Core.Player.ClassLevel > 46)
                {
                    ActionManager.DoAction(221, Core.Player);
                    await Coroutine.Sleep(2000);
                }
            } else if(Core.Player.CurrentJob == ff14bot.Enums.ClassJobType.Miner && !Core.Player.HasAura(222))
            {
                if (Core.Player.ClassLevel > 46)
                {
                    ActionManager.DoAction(238, Core.Player);
                    await Coroutine.Sleep(2000);
                }
            }

            if (Actions.HasAura(AbilityAura.Stealth))
            {
                await Actions.Cast(Ability.Stealth,SpellDelay);
            }

            if(RepairFlag)
            {
                await Repire();
            }

            return isDone = true;
        }

        private async Task<bool> NeedRepair()
        {
            await Coroutine.Sleep(200);
            foreach (var bagslot in InventoryManager.FilledArmorySlots)
            {
                //if(bagslot)
            }

            return false;
        }
        
        private async Task<bool> Repire()
        {
            await CommonTasks.StopAndDismount();

            int flag = 0;

            while (flag++ < 5)
            {
                ActionManager.ToggleRepairWindow();
                if (await Coroutine.Wait(3000, () => Repair.IsOpen))
                {
                    break;
                }
            }

            if (Repair.IsOpen)
            {
                Repair.RepairAll();

                if (await Coroutine.Wait(3000, () => SelectYesno.IsOpen))
                {
                    SelectYesno.ClickYes();

                    await Coroutine.Wait(3000, () => !SelectYesno.IsOpen);
                    await Coroutine.Sleep(4000);
                }
                else
                {
                    Logger.Warn("确认对话框未打开");
                }

                Repair.Close();
            }
            else
            {
                Logger.Warn("修理窗口未打开");
            }

            return true;
        }
    }
}
