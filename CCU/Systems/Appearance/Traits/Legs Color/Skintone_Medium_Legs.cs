using RogueLibsCore;

namespace CCU.Traits.App_LC1
{
	public class Skintone_Medium_Legs : T_LegsColor
	{
		public override string[] Rolls => new string[] { "GoldSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Skintone_Medium_Legs>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este color de piernas a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Skintone_Medium_Legs)),
					[LanguageCode.Spanish] = "Piernas Color Piel Mediana",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
