using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Ghost : T_CCU, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Ghost>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is a spooky, spooky ghost.\nNote: Doesn't work for Player characters."),
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
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.ghost = true;
		}
	}
}