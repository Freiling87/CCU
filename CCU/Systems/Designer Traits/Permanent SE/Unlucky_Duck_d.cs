using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Unlucky_Duck_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.FeelingUnlucky;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Unlucky_Duck_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are unlucky. That's... unlucky. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Unlucky_Duck_d), "Unlucky Duck [D]")
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