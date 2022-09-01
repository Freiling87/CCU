using RogueLibsCore;

namespace CCU.Traits.Facial_Hair
{
	public class No_Facial_Hair : T_FacialHair
	{
		public override string FacialHairType => "None";

        [RLSetup]
		public static void Setup()
		{
			PostProcess = PostProcess = RogueLibs.CreateCustomTrait<No_Facial_Hair>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(No_Facial_Hair)),
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
