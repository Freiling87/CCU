using RogueLibsCore;

namespace CCU.Traits.App_AC1
{
	public class Hat_Blue : T_Accessory
	{
		public override string[] Rolls => new string[] { "HatBlue" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Hat_Blue>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este accesorio a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hat_Blue), "Hat (Blue)"),
					[LanguageCode.Spanish] = "Gorra Azul",
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
