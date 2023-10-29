using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	internal class Extralegal : T_CCU, ISetupAgentStats
	{
		public bool BypassUnlockChecks => false;

		public void SetupAgent(Agent agent)
		{
			agent.noEnforcerAlert = true;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Extralegal>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will not alert enforcers when attacked."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Extralegal)),
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
	}
}