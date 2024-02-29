using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Ghost : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Ghost>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is a spooky, spooky ghost.\nNote: Doesn't work for Player characters." +
					"\n\nBug: Won't path through walls."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ghost)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					Recommendations = { nameof(Ghostbustable) },
					UnlockCost = 0,
				});
		}
		
		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.ghost = true;
			agent.oma.ghost = true;
			agent.oma._ghost = true;
			agent.objectSprite.agentColorDirty = true;
			agent.agentItemColliderTr.gameObject.SetActive(false);
		}
	}
}