using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Thickest_Skin_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.ResistDamageLarge;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Thickest_Skin_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 2. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Thickest_Skin_d), "Thickest Skin [D]")
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