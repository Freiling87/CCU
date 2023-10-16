using RogueLibsCore;

namespace CCU.Traits.Senses
{
    internal class Sharp_Hearing : T_Hearing, ISetupAgentStats
    {
		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Sharp_Hearing>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Can hear agents without Graceful. Loud agents are heard from further away.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Sharp_Hearing)),
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
		}
	}
}