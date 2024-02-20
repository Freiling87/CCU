using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Lucky_Duck_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.FeelingLucky;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Lucky_Duck_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are lucky. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Lucky_Duck_d), "Lucky Duck [D]")
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