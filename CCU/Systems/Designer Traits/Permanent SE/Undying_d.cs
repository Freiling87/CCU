using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Undying_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Resurrection;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Undying_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You resurrect forever. Creepy. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Undying_d), "Undying [D]")
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}	
	}

	[HarmonyPatch(typeof(StatusEffects))]
	public class P_StatusEffects_Undying
	{
		[HarmonyPostfix, HarmonyPatch(nameof(StatusEffects.Resurrect))]
		public static void Resurrect_Undying(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<Undying_d>()
				&& !__instance.hasStatusEffect(VanillaEffects.Resurrection))
				__instance.AddStatusEffect(VanillaEffects.Resurrection);
		}
	}
}