using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Vigilantest : T_Behavior, ISetupAgentStats
	{
		public override bool LosCheck => false;
		public override string[] GrabItemCategories => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Vigilantest>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Reacts to sound like Supercop.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vigilantest)),
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
			agent.modVigilant = 3;
		}
	}
}