using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Enstrongened_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Strength;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Enstrongened_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "FOR EVER STRONK. GRAAAAAAH! Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Enstrongened_d), "Enstrongened [D]")
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