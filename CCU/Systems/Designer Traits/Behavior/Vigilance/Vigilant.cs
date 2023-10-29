using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Vigilant : T_Behavior
	{
		public override void SetupAgent(Agent agent)
		{
			agent.modVigilant = 1;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Vigilant>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Reacts to sound like Shopkeeper, Slavemaster & Soldier.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vigilant)),
				})
				.WithUnlock(new TU_DesignerUnlock
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
	}
}