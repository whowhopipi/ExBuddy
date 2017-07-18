namespace ExBuddy.OrderBotTags.WhoFish
{
    using Buddy.Coroutines;
    using Clio.Utilities;
    using Clio.XmlEngine;
    using ExBuddy;
    using ExBuddy.Helpers;
    using ExBuddy.OrderBotTags.Fish;
    using ff14bot;
    using ff14bot.Behavior;
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface IWhoFishSpot
    {
        Vector3 NodeLocation { get; set; }

        Task<bool> MoveFromSpot(WhoFishTag tag);

        Task<bool> MoveToSpot(WhoFishTag tag);

        FishStrategy FishType { get; set; }

		float Heading { get; set; }
        
        int MinFish { get; set; }

        int MaxFish { get; set; }

		bool Sit { get; set; }
        
	}

	[XmlElement("WhoFishSpot")]
	public class WhoFishSpot : IWhoFishSpot
    {
		public WhoFishSpot()
		{
            NodeLocation = Vector3.Zero;
			Heading = 0f;
		}

		public WhoFishSpot(string xyz, float heading)
		{
            NodeLocation = new Vector3(xyz);
			Heading = heading;
		}

		public WhoFishSpot(Vector3 xyz, float heading)
		{
            NodeLocation = xyz;
			Heading = heading;
		}

		[DefaultValue(true)]
		[XmlAttribute("UseMesh")]
		public bool UseMesh { get; set; }

		public override string ToString()
        {
            return string.Format("FishSpot:UseMesh[{0}],Location[{1}],Heading[{2}],FishType[{3}],Radius[{4}]",
                UseMesh, NodeLocation, Heading, FishType,Radius);
		}

		#region IFishSpot Members

		[XmlAttribute("Heading")]
		public float Heading { get; set; }

		[XmlAttribute("XYZ")]
		[XmlAttribute("Location")]
		public Vector3 NodeLocation { get; set; }
        
        [XmlAttribute("From")]
        public Vector3 FromNode { get; set; }

        [DefaultValue(false)]
		[XmlAttribute("Sit")]
		public bool Sit { get; set; }

        [DefaultValue(false)]
        [XmlAttribute("Stealth")]
        public bool Stealth { set; get; }

        [DefaultValue(-1)]
        [XmlAttribute("MaxFish")]
        public int MaxFish { set; get; }
        
        [DefaultValue(-1)]
        [XmlAttribute("MinFish")]
        public int MinFish { set; get; }

        [DefaultValue(FishStrategy.GatherOrCollect)]
        [XmlAttribute("FishType")]
        public FishStrategy FishType { set; get; }

        [DefaultValue(0.5f)]
        [XmlAttribute("Radius")]
        public float Radius { get; set; }

        public virtual async Task<bool> MoveFromSpot(WhoFishTag tag)
        {
            tag.StatusText = "Moving from " + this;

            await Coroutine.Sleep(200);

            return true;
		}

		public virtual async Task<bool> MoveToSpot(WhoFishTag tag)
        {
            tag.StatusText = "Moving to " + this;


            var result = true;

            if(FromNode != null && (FromNode.X !=0 && FromNode.Y != 0 && FromNode.Z != 0))
            {
                result = await
                     FromNode.MoveTo(
                         UseMesh,
                         radius: Radius,
                         name: "Move to Location",
                         stopCallback: tag.MovementStopCallback,
                         dismountAtDestination: true);
            }

            await CommonTasks.StopAndDismount();

            if (Stealth && !Actions.HasAura(AbilityAura.Stealth))
            {
                await Actions.CastAura(FishActions.Stealth, tag.SpellDelay,(int)AbilityAura.Stealth);
            } else if(!Stealth && Actions.HasAura(AbilityAura.Stealth))
            {
                await Actions.CastAura(FishActions.Stealth, tag.SpellDelay);
            }

            result = result && await
                    NodeLocation.MoveTo(
                        UseMesh,
                        radius: Radius,
                        name: "Move to Location",
                        stopCallback: tag.MovementStopCallback,
                        dismountAtDestination: true);

            return true;
		}

		#endregion
        
	}

	public class WhoStealthApproachFishSpot : WhoFishSpot
    {
		[DefaultValue(true)]
		[XmlAttribute("ReturnToStealthLocation")]
		public bool ReturnToStealthLocation { get; set; }

		[XmlAttribute("StealthLocation")]
		public Vector3 StealthLocation { get; set; }

		[XmlAttribute("UnstealthAfter")]
		public bool UnstealthAfter { get; set; }

		public override async Task<bool> MoveFromSpot(WhoFishTag tag)
		{
			tag.StatusText = "Moving from " + this;

			var result = true;
			if (ReturnToStealthLocation)
			{
				result &= await StealthLocation.MoveToNoMount(
                    UseMesh, 
                    Radius, 
                    "Stealth Location", 
                    tag.MovementStopCallback);
			}

			if (UnstealthAfter && Core.Player.HasAura((int) AbilityAura.Stealth))
			{
                result &= await Actions.CastAura(FishActions.Stealth,tag.SpellDelay);
			}

			return result;
		}

		public override async Task<bool> MoveToSpot(WhoFishTag tag)
		{
			tag.StatusText = "Moving to " + this;

			if (StealthLocation == Vector3.Zero)
			{
				return false;
			}

			var result =
				await
					StealthLocation.MoveTo(
						UseMesh,
						radius: Radius,
						name: "Stealth Location",
						stopCallback: tag.MovementStopCallback,
						dismountAtDestination: true);

			if (result)
			{
				await Coroutine.Yield();
				if (!Core.Player.HasAura((int) AbilityAura.Stealth))
				{
                    await Actions.CastAura(FishActions.Stealth,tag.SpellDelay);
				}

				result = await NodeLocation.MoveToNoMount(
                    UseMesh, 
                    Radius, 
                    tag.Name, 
                    tag.MovementStopCallback);
			}

			return result;
		}

		public override string ToString()
		{
			return this.DynamicToString("UnstealthAfter");
		}
	}

	public class WhoIndirectApproachFishSpot : FishSpot {}
}