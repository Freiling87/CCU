using CCU.Traits.App_BC1;
using RogueLibsCore;

namespace CCU.Traits.App_BC3
{
	public class Shirtsome : T_BodyColor
	{
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Shirtsome>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Prevents body from matching skin color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shirtsome)),
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
