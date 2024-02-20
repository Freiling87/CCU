using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Regenerationist_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.RegenerateHealth;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Regenerationist_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently regenerating. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Regenerationist_d), "Regenerationist [D]")
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