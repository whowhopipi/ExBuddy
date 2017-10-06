namespace ExBuddy.Helpers
{
	using Buddy.Coroutines;
	using ExBuddy.Logging;
	using ff14bot;
	using ff14bot.Managers;
	using ff14bot.Objects;
	using System.Threading.Tasks;

	internal static class Actions
	{
		internal static async Task<bool> Cast(uint id, int delay)
		{
			//TODO: check affinity, cost type, spell type, and add more informational logging and procedures to casting
			//Wait till we can cast the spell
			SpellData spellData;
			if (GatheringManager.ShouldPause(spellData = DataManager.SpellCache[id]))
			{
				await Coroutine.Wait(3500, () => !GatheringManager.ShouldPause(spellData));
			}

			var result = ActionManager.DoAction(id, Core.Player);

			var ticks = 0;
			while (result == false && ticks++ < 10 && Behaviors.ShouldContinue)
			{
				result = ActionManager.DoAction(id, Core.Player);
				await Coroutine.Yield();
			}

			if (result)
			{
				Logger.Instance.Info("Casted Ability -> {0}", spellData.Name);
			}
			else
			{
				Logger.Instance.Error("Failed to cast Ability -> {0}", spellData.Name);
			}

			//Wait till we can cast again
			if (GatheringManager.ShouldPause(spellData))
			{
				await Coroutine.Wait(3500, () => !GatheringManager.ShouldPause(spellData));
			}
			if (delay > 0)
			{
				await Coroutine.Sleep(delay);
			}
			else
			{
				await Coroutine.Yield();
			}

			return result;
		}

		internal static async Task<bool> Cast(Ability ability, int delay)
		{
			return await Cast(Abilities.Map[Core.Player.CurrentJob][ability], delay);
		}

		internal static async Task<bool> CastAura(uint spellId, int delay, int auraId = -1)
		{
			var result = false;
			if (auraId == -1 || !Core.Player.HasAura((uint) auraId))
			{
				SpellData spellData;
				if (GatheringManager.ShouldPause(spellData = DataManager.SpellCache[spellId]))
				{
					await Coroutine.Wait(3500, () => !GatheringManager.ShouldPause(DataManager.SpellCache[spellId]));
				}

				result = ActionManager.DoAction(spellId, Core.Player);
				var ticks = 0;
				while (result == false && ticks++ < 5 && Behaviors.ShouldContinue)
				{
					result = ActionManager.DoAction(spellId, Core.Player);
					await Coroutine.Yield();
				}

				if (result)
				{
					Logger.Instance.Info("Casted Aura -> {0}", spellData.Name);
				}
				else
				{
					Logger.Instance.Error("Failed to cast Aura -> {0}", spellData.Name);
				}

				//Wait till we have the aura
				await Coroutine.Wait(3500, () => Core.Player.HasAura((uint) auraId));
				if (delay > 0)
				{
					await Coroutine.Sleep(delay);
				}
				else
				{
					await Coroutine.Yield();
				}
			}

			return result;
		}

		internal static async Task<bool> CastAura(Ability ability, int delay, AbilityAura aura = AbilityAura.None)
		{
			return await CastAura(Abilities.Map[Core.Player.CurrentJob][ability], delay, (int)aura);
		}

        internal static bool CanCast(uint id)
        {
            return ActionManager.CanCast(id, Core.Me);
        }

        internal static bool CanCast(Ability ability)
        {
            return CanCast(Abilities.Map[Core.Player.CurrentJob][ability]);
        }

        internal static async Task<bool> HasAction(Ability ability)
        {
            await Coroutine.Yield();
            bool result = ActionManager.CurrentActions.ContainsKey(Abilities.Map[Core.Player.CurrentJob][ability]);

            var tickit = 0;
            while (result == false && tickit < 6)
            {
                await Coroutine.Sleep(5000);
                result = ActionManager.CurrentActions.ContainsKey(Abilities.Map[Core.Player.CurrentJob][ability]);
            }

            return result;
        }

        public static bool HasAura(AbilityAura auraId)
        {
            return HasAura((uint)auraId);
        }

        public static bool HasAura(uint auraId)
        {
            return Core.Me.HasAura(auraId);
        }

    }
}