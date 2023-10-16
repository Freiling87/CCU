using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Vigilanter : T_Behavior, ISetupAgentStats
	{
		public override bool LosCheck => false;
		public override string[] GrabItemCategories => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Vigilanter>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Reacts to sound like Bartender, Bouncer, Cannibal & Goon.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vigilanter)),
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
			agent.modVigilant = 2;
		}
	}
}