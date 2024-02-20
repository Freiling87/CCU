using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Bulletproofish_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.ResistBulletsSmall;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Bulletproofish_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Bullet damage permanently reduced by 1/3. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bulletproofish_d), "Bulletproofish [D]")
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