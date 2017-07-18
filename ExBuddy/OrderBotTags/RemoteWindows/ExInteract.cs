namespace ExBuddy.OrderBotTags.RemoteWindows
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ExBuddy.Helpers;
    using ff14bot.Managers;
    using ff14bot.Objects;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [LoggerName("ExInteract")]
    [XmlElement("ExInteract")]
    public class ExInteract : ExRemoteWindowsBase
    {
        [XmlAttribute("ObjectId")]
        public uint ObjectId { set; get; }

        [XmlAttribute("NpcId")]
        public uint NpcId { set; get; }

        [XmlAttribute("NpcName")]
        public string NpcName { set; get; }

        [DefaultValue(4)]
        [XmlAttribute("Radius")]
        public float Radius { set; get; }
        
        private GameObject obj = null;
        
        protected override void OnStart()
        {
            base.OnStart();

            obj = null;
        }
        
        protected override async Task<bool> Main()
        {
            if(ObjectId != 0)
            {
                obj = GameObjectManager.GetObjectByObjectId(ObjectId);
            }

            if(obj == null && NpcId != 0)
            {
                obj = GameObjectManager.GetObjectsByNPCId(NpcId).OrderBy(go => go.Location.Distance2D(Me.Location)).FirstOrDefault();
            }

            if(obj == null && NpcName != null && !NpcName.Equals(""))
            {
                obj = GameObjectManager.GameObjects.Where(go => go.Name.Contains(NpcName)).OrderBy(go => go.Location.Distance2D(Me.Location)).FirstOrDefault();
            }

            if(obj == null)
            {
                return false;
            }

            if(obj.Distance(Me.Location) > Radius)
            {
                await obj.Location.MoveTo(radius: Radius);
            }

            obj.Interact();

            await Coroutine.Yield();

            await Coroutine.Wait(1000, () => Me.IsCasting);

            if (Me.IsCasting)
            {
                await Coroutine.Wait(Timeout.InfiniteTimeSpan,() => !Me.IsCasting);
                await Coroutine.Yield();
            }

            isDone = true;

            return true;
        }
        
    }
}
