using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Strong_Immune_System_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.StableSystem;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Strong_Immune_System_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You're immune to negative status effects. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Strong_Immune_System_d), "Strong Immune System [D]")
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