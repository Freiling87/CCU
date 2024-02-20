using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Ragestart_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Rage;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Ragestart_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are permanently enraged. Uh... good luck? Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ragestart_d), "Ragestart [D]")
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
}