using CCU.Traits.App_BC1;
using RogueLibsCore;

namespace CCU.Traits.App_BC3
{
	public class Shirtless : T_BodyColor
	{
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Shirtless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes body to match skin color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shirtless)),
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
