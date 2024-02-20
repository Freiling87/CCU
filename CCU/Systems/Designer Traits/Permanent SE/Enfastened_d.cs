using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Enfastened_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Fast;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Enfastened_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Speed increased by 50%. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Enfastened_d), "Enfastened [D]")
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