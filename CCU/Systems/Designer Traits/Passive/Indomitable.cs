using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Indomitable : T_CCU, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Indomitable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Immune to mind control."),
					[LanguageCode.Spanish] = "Este NPC es inmune al control mental.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Indomitable)),
					[LanguageCode.Spanish] = "Indominable",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
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
			agent.preventsMindControl = true;
		}
	}
}