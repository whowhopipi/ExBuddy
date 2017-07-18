using Buddy.Coroutines;
using Clio.XmlEngine;
using ExBuddy.Attributes;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [LoggerName("ExRepair")]
    [XmlElement("ExRepair")]
    public class ExRepair : ExProfileBehavior
    {
 
        protected override async Task<bool> Main()
        {
            StatusText = string.Format("修理装备");

            int flag = 0;

            while(flag++ < 5)
            {
                ActionManager.ToggleRepairWindow();
                if(await Coroutine.Wait(3000, () => Repair.IsOpen))
                {
                    break;
                }
            }

            if (Repair.IsOpen)
            {
                Repair.RepairAll();

                if(await Coroutine.Wait(3000,() => SelectYesno.IsOpen))
                {
                    SelectYesno.ClickYes();

                    await Coroutine.Wait(3000, () => !SelectYesno.IsOpen);
                    await Coroutine.Sleep(4000);
                } else
                {
                    Logger.Warn("确认对话框未打开");
                }

                Repair.Close();
            } else
            {
                Logger.Warn("修理窗口未打开");
            }

            return isDone = true;
        }
        
    }
}
