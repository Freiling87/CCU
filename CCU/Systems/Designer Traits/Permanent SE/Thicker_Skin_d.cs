using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Thicker_Skin_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.ResistDamageMedium;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Thicker_Skin_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 1.5. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Thicker_Skin_d), "Thicker Skin [D]")
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