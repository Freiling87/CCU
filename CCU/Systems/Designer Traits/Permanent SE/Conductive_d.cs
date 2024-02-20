using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Conductive_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.ElectroTouch;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Conductive_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanent Electro Touch. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Conductive_d), "Conductive [D]")
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