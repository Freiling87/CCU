using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Above_the_Laws_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.AbovetheLaw;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Above_the_Laws_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently Above the Law...s. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Above_the_Laws_d), "Above The Laws [D]")
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