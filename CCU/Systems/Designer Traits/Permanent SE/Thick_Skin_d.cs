using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Thick_Skin_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.ResistDamageSmall;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Thick_Skin_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 1.25. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Thick_Skin_d), "Thick Skin [D]")
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