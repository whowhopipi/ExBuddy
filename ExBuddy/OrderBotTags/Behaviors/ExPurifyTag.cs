﻿namespace ExBuddy.OrderBotTags.Behaviors
{
	using Buddy.Coroutines;
	using Clio.Utilities;
	using Clio.XmlEngine;
	using ExBuddy.Attributes;
	using ExBuddy.Helpers;
	using ExBuddy.Windows;
	using ff14bot.Behavior;
	using ff14bot.Managers;
	using ff14bot.Navigation;
	using ff14bot.RemoteWindows;
	using System.ComponentModel;
	using System.Threading.Tasks;
	using PurifyDialog = ExBuddy.Windows.PurifyDialog;

	[LoggerName("ExPurify")]
	[XmlElement("ExPurify")]
	[XmlElement("ExReduce")]
	public class ExPurifyTag : ExProfileBehavior
	{
		[DefaultValue("True")]
		[XmlAttribute("Condition")]
		public string Condition { get; set; }

		[DefaultValue(5000)]
		[XmlAttribute("MaxWait")]
		public int MaxWait { get; set; }

		protected override async Task<bool> Main()
		{
			if (!ScriptManager.GetCondition(Condition)())
			{
				Logger.Info(Localization.Localization.ExPurify_GetCondition, Condition);
				return isDone = true;
			}

			await Behaviors.Wait(2000, () => !Window<Gathering>.IsOpen);
			await Behaviors.Wait(2000, () => !GatheringMasterpiece.IsOpen);

			Navigator.Stop();

			if (MovementManager.IsFlying && !MovementManager.IsDiving)
			{
				Logger.Error(Localization.Localization.ExPurify_Land);
				return isDone = true;
			}

			await CommonTasks.StopAndDismount();

			if (await Coroutine.Wait(
				MaxWait,
				() =>
				{
					if (!ExProfileBehavior.Me.IsMounted)
					{
						return true;
					}

					ActionManager.Dismount();
					return false;
				}))
			{
				await PurifyDialog.ReduceAllItems(InventoryManager.FilledSlots, (ushort)MaxWait);
			}
			else
			{
				Logger.Error(Localization.Localization.ExPurify_Dismount);
			}

			return isDone = true;
		}

		protected override void OnStart()
		{
			MaxWait = MaxWait.Clamp(1000, 10000);
		}
	}
}