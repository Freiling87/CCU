using CCU.Traits.App_HS1;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.App_HS3
{
    public class Mask_Override : T_Hairstyle
	{
		public override string[] Rolls => new string[] { };

		public static List<string> IncompatibleAccessories = new List<string>() { "CopHat", "DoctorHeadLamp", "HatBlue", "HatRed", "ThiefHat", "Fedora", "FireHelmet", "Cop2Hat", "Headphones" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Mask_Override>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Normally, masks deactivate most accessories except eyewear. This disables that function.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mask_Override)),
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