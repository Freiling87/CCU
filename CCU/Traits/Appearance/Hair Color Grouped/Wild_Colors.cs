using CCU.Traits.Hair_Color;
using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hair_Color_Grouped
{
    public class Wild_Colors : T_HairColor
	{
		public override string[] HairColors => new string[] { "Red", "Green", "Purple", "Pink", "Blue" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Wild_Colors>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hairstyles to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wild_Colors)),
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