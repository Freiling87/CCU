using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	public class Grab_Drugs : T_ItemPickup
	{
		public override string[] GrabItemCategories => new string[] { VItemCategory.Drugs };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Grab_Drugs>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab drugs if they see any."),
					[LanguageCode.Spanish] = "Este NPC agarra todo tipo de narcotico o sustancia muy illegal que encuentre, como ese primo raro tuyo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Drugs)),
					[LanguageCode.Spanish] = "Colleciona Drogas",
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