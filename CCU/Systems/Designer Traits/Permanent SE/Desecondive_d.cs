using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Desecondive_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Shrunk;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Desecondive_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently 1/60 the size of someone who's Diminutive. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Desecondive_d), "Desecondive [D]")
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