using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS2
{
    public class Short_Female_Styles : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "Leia", "Ponytail" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Short_Female_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Short_Female_Styles)),
				})
				.WithUnlock(new TraitUnlock_CCU
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