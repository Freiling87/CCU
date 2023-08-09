using CCU.Traits.App;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Traits.App_SS3
{
	public class Shadowless : T_Appearance, ISetupAgentStats
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Shadowless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shadowless)),
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
			Owner.agentHitboxScript.shadow.GetComponent<MeshRenderer>().enabled = false;
		}
		public override void OnRemoved()
		{
			Owner.agentHitboxScript.shadow.GetComponent<MeshRenderer>().enabled = true;
		}
		public void SetupAgentStats(Agent agent)
		{
			Owner.agentHitboxScript.shadow.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}