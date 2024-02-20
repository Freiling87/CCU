using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Thickester_Skin_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.NumbtoPain;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Thickester_Skin_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 3. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Thickester_Skin_d), "Thickester Skin [D]")
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