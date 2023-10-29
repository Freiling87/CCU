using CCU.Traits.App;
using RogueLibsCore;

namespace CCU.Traits.App_SS3
{
	public class Bodyless : T_Appearance, ISetupAgentStats
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Bodyless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "That's weird, there's no body here!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bodyless)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() 
		{
			Owner.agentHitboxScript.bodyAnim.gameObject.SetActive(false);
		}
		public override void OnRemoved() 
		{
			Owner.agentHitboxScript.bodyAnim.gameObject.SetActive(true);
		}
		public void SetupAgentStats(Agent agent)
		{
			agent.agentHitboxScript.bodyAnim.gameObject.SetActive(false);
		}
	}
}