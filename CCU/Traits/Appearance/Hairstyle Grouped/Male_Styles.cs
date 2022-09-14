using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hairstyle_Grouped
{
    public class Male_Styles : T_Hairstyle
	{
		public override string[] HairstyleType => new string[] { "Military", "Normal", "NormalHigh", "Suave", "Pompadour", "Spiky", "SpikyShort", "Mohawk", "Bald", "Balding", "Sidewinder", "Curtains", "Afro", "PuffyShort" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Male_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Male_Styles)),
				})
				.WithUnlock(new TraitUnlock
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