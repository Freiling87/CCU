using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Fleshy_Follicles : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Fleshy_Follicles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Matches hair color to skin color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fleshy_Follicles)),
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
