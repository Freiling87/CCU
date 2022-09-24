using CCU.Traits.App_EC1;
using RogueLibsCore;

namespace CCU.Traits.App_EC3
{
	public class Beady_Eyed : T_EyeColor
    {
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Beady_Eyed>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Matches Eye Color to Body Color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Beady_Eyed), "Beady-Eyed"),
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
