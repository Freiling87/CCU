using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Grab_Drugs : T_Behavior, ISetupAgentStats
	{
		public override bool LosCheck => true;
		public override string[] GrabItemCategories => new string[] { VItemCategory.Drugs };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Grab_Drugs>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab drugs if they see any."),
					[LanguageCode.Spanish] = "Este NPC agarra todo tipo de narcotico o sustancia muy illegal que encuentre, como ese primo raro tuyo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Drugs)),
					[LanguageCode.Spanish] = "Colleciona Drogas",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public void SetupAgentStats(Agent agent)
		{
			agent.losCheckAtIntervals = true;
		}
	}
}
