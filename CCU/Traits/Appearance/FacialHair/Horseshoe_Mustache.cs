using RogueLibsCore;

namespace CCU.Traits.Facial_Hair
{
	public class Hangdog_Mustache : T_FacialHair
	{
        public override string FacialHairType => "MustacheRedneck";

        [RLSetup]
		public static void Setup()
		{
			PostProcess = PostProcess = RogueLibs.CreateCustomTrait<Hangdog_Mustache>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Hangdog_Mustache)),
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
