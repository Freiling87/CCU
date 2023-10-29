using CCU.Traits.App;
using RogueLibsCore;

namespace CCU.Traits.App_SS3
{
	public class Eyeless : T_Appearance, ISetupAgentStats
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Eyeless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "They can still inexplicably shoot you.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Eyeless)),
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
			Owner.agentHitboxScript.eyes.color = new UnityEngine.Color32(0, 0, 0, 0);
		}
		public override void OnRemoved() 
		{
		}
		public void SetupAgentStats(Agent agent)
		{
			Owner.agentHitboxScript.eyes.color = new UnityEngine.Color32(0, 0, 0, 0);
		}
	}
}