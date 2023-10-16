using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Grab_Food : T_Behavior, ISetupAgentStats
	{
		public override bool LosCheck => true;
		public override string[] GrabItemCategories => new string[] { VItemCategory.Food };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Grab_Food>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab food if they see it."),
					[LanguageCode.Spanish] = "Este NPC agarra toda comida en el suelo que vea. porque es un cochino, UN COCHINO!!!.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Food)),
					[LanguageCode.Spanish] = "Colleciona Comida",
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
