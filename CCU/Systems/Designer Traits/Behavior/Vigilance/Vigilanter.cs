using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Vigilanter : T_Behavior
	{
		public override void SetupAgent(Agent agent)
		{
			agent.modVigilant = 2;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Vigilanter>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Reacts to sound like Bartender, Bouncer, Cannibal & Goon.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vigilanter)),
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