using CCU.Traits.Hair_Color;
using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hair_Color_Grouped
{
    public class Normal_Colors : T_HairColor
	{
		public override string[] HairColors => new string[] { "Brown", "Black", "Blonde", "Orange", "Grey" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Normal_Colors>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds normal human hair colors to the pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_Colors)),
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