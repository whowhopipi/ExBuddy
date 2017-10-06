using Buddy.Coroutines;
using Clio.XmlEngine;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using System.Threading.Tasks;

namespace ExBuddy.OrderBotTags.Behaviors
{
    [XmlElement("ExUseObject")]
    public class ExUseObject : ExProfileBehavior
    {

        [XmlAttribute("NpcId")]
        public uint NpcId { get; set; }

        protected override Task<bool> DoMainSuccess()
        {
            var obj = GameObjectManager.GetObjectByNPCId(NpcId);

            isDone = obj == null;
            return base.DoMainSuccess();
        }

        protected async override Task<bool> Main()
        {
            var obj = GameObjectManager.GetObjectByNPCId(NpcId);

            if(obj == null)
            {
                return true;
            }

            obj.Interact();
            
            if(await Coroutine.Wait(1000,() => SelectYesno.IsOpen))
            {
                SelectYesno.ClickYes();

                await Coroutine.Wait(1000, () => !SelectYesno.IsOpen);
            }

            if(await Coroutine.Wait(1000, () => CommonBehaviors.IsLoading))
            {
                await CommonTasks.HandleLoading();
            }

            return true;
        }
    }
}
