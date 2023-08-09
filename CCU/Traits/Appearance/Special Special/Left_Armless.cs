using CCU.Traits.App;
using RogueLibsCore;

namespace CCU.Traits.App_SS3
{
	public class Left_Armless : T_Appearance, ISetupAgentStats
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Left_Armless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Wouldn't hurt a fly, try as they might.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Left_Armless)),
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
			Owner.agentHitboxScript.skinColor = new UnityEngine.Color32(0, 0, 0, 0);
		}
		public override void OnRemoved() 
		{
		}
		public void SetupAgentStats(Agent agent)
		{
			Owner.agentHitboxScript.skinColor = new UnityEngine.Color32(0, 0, 0, 0);
		}
	}
}