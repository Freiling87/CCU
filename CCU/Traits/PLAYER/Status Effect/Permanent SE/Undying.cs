using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Undying : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.Resurrection;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Undying>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You resurrect forever. Creepy.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Undying))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {  },
					CharacterCreationCost = 100,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 0,
					Upgrade = null,
					Unlock =
					{
						removal = false,
						categories = { }
					}
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(StatusEffects))]
	internal class P_StatusEffects_Undying
	{
		[HarmonyPostfix,HarmonyPatch(nameof(StatusEffects.Resurrect))]
		public static void Resurrect_Undying(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<Undying>()
				&& !__instance.hasStatusEffect(VanillaEffects.Resurrection))
				__instance.AddStatusEffect(VanillaEffects.Resurrection);
		}
	}
}