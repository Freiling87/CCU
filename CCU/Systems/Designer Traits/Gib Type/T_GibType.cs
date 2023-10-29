using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Linq;
using System.Reflection;

namespace CCU.Traits.Gib_Type
{
	public abstract class T_GibType : T_CCU
	{
		public T_GibType() : base() { }

		public abstract string audioClipName { get; }
		public abstract DecalSpriteName gibDecal { get; }
		public abstract int gibQuantity { get; }
		public abstract int gibSpriteIteratorLimit { get; }
		public abstract GibSpriteNameStem gibType { get; }
		public abstract string particleEffect { get; }

		// These names need to match Gib sprite names
		public enum DecalSpriteName
		{
			BloodExplosion,
			BloodExplosionGhost, // This one might break if certain things are changed. See vanilla SpawnerMain.SpawnFloorDecal for details.
			ExplosionScorchMark,
			SlimePuddle,

			None
		}
		public enum GibSpriteNameStem
		{
			//GibletAsh, // TODO
			GibletGhost,
			GibletGlass,
			GibletIce,
			GibletNone,
			GibletNormal,
			GibletRobot,

			// Wall wreckage sprites not loading, alternatives on right.
			WallCaveWreckage, FlamingBarrelWreckage,
			WallGlassWreckage, WindowWreckage,
			WallHedgeWreckage, BushWreckage,
		}

		public static GibSpriteNameStem GetGibType(Agent agent) =>
			agent.GetTraits<T_GibType>().FirstOrDefault()?.gibType ??
				GibSpriteNameStem.GibletNormal;

		public static void CustomGib(StatusEffects statusEffects)
		{
			Agent agent = statusEffects.agent;
			T_GibType gibTrait = agent.GetTraits<T_GibType>().FirstOrDefault();

			if (statusEffects.slaveHelmetGonnaBlow)
			{
				MethodInfo slaveHelmetBlow = AccessTools.DeclaredMethod(typeof(StatusEffects), "SlaveHelmetBlow");
				slaveHelmetBlow.GetMethodWithoutOverrides<Action>(statusEffects).Invoke();
				return;
			}

			if ((GC.serverPlayer && !statusEffects.agent.disappeared) || (!GC.serverPlayer && !statusEffects.agent.gibbed && !statusEffects.agent.fellInHole))
			{
				statusEffects.agent.gibbed = true;
				statusEffects.Disappear();

				if (agent.HasTrait<Gibless>())
					return;

				if (GC.bloodEnabled)
				{
					if ((!statusEffects.agent.overHole || statusEffects.agent.underWater) && (!statusEffects.agent.warZoneAgent || !statusEffects.agent.underWater))
					{
						InvItem invItem = new InvItem();
						invItem.invItemName = "Giblet";
						invItem.SetupDetails(false);
						string gibSpriteName = T_GibType.GetGibType(statusEffects.agent).ToString();

						for (int i = 1; i <= gibTrait.gibQuantity; i++)
						{
							int gibSpriteIterator = Math.Max(1, i % (gibTrait.gibSpriteIteratorLimit + 1));
							invItem.LoadItemSprite(gibSpriteName + gibSpriteIterator);
							GC.spawnerMain.SpawnWreckage(statusEffects.agent.tr.position, invItem, statusEffects.agent, null, false);
						}

						if (!statusEffects.agent.underWater && gibTrait.gibDecal != T_GibType.DecalSpriteName.None)
							GC.spawnerMain.SpawnFloorDecal(statusEffects.agent.tr.position, gibTrait.gibDecal.ToString());
					}
					if (!statusEffects.dontDoBloodExplosion && !(gibTrait.particleEffect is null)) // dontdobloodexplosion is strictly tied to cannibalism
					{
						GC.spawnerMain.SpawnParticleEffect(gibTrait.particleEffect, statusEffects.agent.tr.position, 0f);
						GC.audioHandler.Play(statusEffects.agent, gibTrait.audioClipName);
					}
					else
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.CannibalFinish);// This is specific to cannibalize; not sure if that will ever lead here but just in case.
				}
				else // I doubt many players play with gore disabled, but who knows
				{
					GC.spawnerMain.SpawnParticleEffect("WallDestroyed", statusEffects.agent.tr.position, 0f);

					if (statusEffects.agent.agentName == VanillaAgents.Robot)
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.RobotDeath);
					else
						GC.audioHandler.Play(statusEffects.agent, VanillaAudio.AgentDie);
				}

				if (statusEffects.agent.isPlayer != 0 && !statusEffects.playedPlayerDeath)
					GC.audioHandler.Play(statusEffects.agent, VanillaAudio.PlayerDeath);
			}
		}
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent_SetupAgentStats
	{
		// TODO: ISetupAgentStats
		[HarmonyPostfix, HarmonyPatch(nameof(Agent.SetupAgentStats))]
		public static void ApplySetupAgentStats(Agent __instance)
		{
			if (!__instance.GetTraits<T_GibType>().Any())
				__instance.AddTrait<Meat_Chunks>();
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.NormalGib))]
		public static bool NormalGib_Redirect(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<Indestructible>())
				return false;

			if (__instance.agent.HasTrait<Meat_Chunks>() || !__instance.agent.GetTraits<T_GibType>().Any())
				return true;

			T_GibType.CustomGib(__instance);
			return false;
		}
	}
}