using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using UnityEngine.Networking;

namespace CCU.Status_Effects
{
	[EffectParameters(EffectLimitations.RemoveOnDeath | EffectLimitations.RemoveOnKnockOut)]
	public class Frozen_Fragile : CustomEffect
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomEffect<Frozen_Fragile>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Frozen (Fragile)",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "A delicate ice sculpture or some shit.",
				});
		}

		public override int GetEffectHate() => 5;
		public override int GetEffectTime() => 9999;
		public override void OnAdded()
		{
			Owner.frozen = true;
			Owner.objectSprite.frozen = true;
			Owner.objectSprite.agentColorDirty = true;
			Owner.statusEffects.CantDoAnything(VanillaEffects.Frozen);
			GC.spawnerMain.SpawnStateIndicator(Owner, "NoAnim");
			Owner.Say("", true);
		}
		public override void OnRemoved()
		{
			if (!Owner.dead)
				Owner.frozen = false;

			if (Owner.isPlayer > 0 && Owner.localPlayer)
				GC.playerControl.SetCantPressGameplayButtons(VanillaEffects.Frozen, 0, Owner.isPlayer - 1);

			Owner.objectSprite.frozen = false;
			Owner.objectSprite.agentColorDirty = true;
			Owner.objectSprite.SetRenderer("Off");
			GC.tileInfo.DirtyWalls();
		}
		public override void OnUpdated(EffectUpdatedArgs e) { }
	}

	[HarmonyPatch(typeof(StatusEffects))]
	public static class P_StatusEffects_FrozenFragile
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(StatusEffects.ChangeHealth),
			argumentTypes: new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		public static void ChangeHealth_FrozenFragile(StatusEffects __instance)
		{
			if (__instance.agent.HasEffect<Frozen_Fragile>())
			{
				__instance.agent.frozen = true;
				__instance.IceGib();
			}
		}
	}
}