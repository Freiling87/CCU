using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Slothful_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Slow;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Slothful_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently slowed. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slothful_d), "Slothful [D]")
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