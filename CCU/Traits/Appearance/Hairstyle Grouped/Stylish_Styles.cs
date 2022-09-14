using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hairstyle_Grouped
{
    public class Stylish_Styles : T_Hairstyle
	{
		public override string[] HairstyleType => new string[] { "Suave", "Pompadour", "Spiky", "SpikyShort" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Stylish_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Stylish_Styles)),
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