using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class The_Invincibility_Gambit_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Invincible;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<The_Invincibility_Gambit_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ah, you clever strategist, you!\n\nNO NUGGETS EVER. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(The_Invincibility_Gambit_d), "The Invincibility Gambit [D]")
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