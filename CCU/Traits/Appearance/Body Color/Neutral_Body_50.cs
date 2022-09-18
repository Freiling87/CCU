using RogueLibsCore;

namespace CCU.Traits.App_BC1
{
	public class Neutral_Body_50 : T_BodyColor
	{
        public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Neutral_Body_50>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"Neutral Body\" 50% of the time, regardless of the number of other items in the pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Neutral_Body_50), "Neutral Body 50%"),
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
