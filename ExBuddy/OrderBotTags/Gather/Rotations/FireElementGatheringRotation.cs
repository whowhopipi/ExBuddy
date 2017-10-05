﻿namespace ExBuddy.OrderBotTags.Gather.Rotations
{
	using Attributes;
	using ff14bot;
	using ff14bot.Managers;
	using System.Threading.Tasks;

	[GatheringRotation("FireElement", 30, 400)]
	public sealed class FireElementGatheringRotation : SmartGatheringRotation
	{
		public override async Task<bool> ExecuteRotation(ExGatherTag tag)
		{
			if (Core.Player.CurrentGP > 399)
			{
				await Wait();
				ActionManager.DoAction(234U, Core.Player);
			}

			return await base.ExecuteRotation(tag);
		}
	}
}