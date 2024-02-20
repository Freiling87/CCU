using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Gigantic_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Giant;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Gigantic_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently giant. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gigantic_d), "Gigantic [D]")
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