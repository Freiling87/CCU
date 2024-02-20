using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Invisibility_Enjoyer_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Invisible;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Invisibility_Enjoyer_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently Invisible. Enjoy. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Invisibility_Enjoyer_d), "Invisibility Enjoyer [D]")
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