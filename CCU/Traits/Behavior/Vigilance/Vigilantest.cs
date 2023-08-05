using RogueLibsCore;

namespace CCU.Traits.Behavior
{
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Shark_Eyes.cs
    public class Horse_Eyes : T_Senses, ISetupAgentStats
========
	public class Vigilantest : T_Behavior, ISetupAgentStats
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilantest.cs
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => new string[] { };

        [RLSetup]
        public static void Setup()
        {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Shark_Eyes.cs
            PostProcess = RogueLibs.CreateCustomTrait<Horse_Eyes>()
========
            PostProcess = RogueLibs.CreateCustomTrait<Vigilantest>()
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilantest.cs
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sets vision cone to 180 degrees (vanilla value = 96).",
                })
                .WithName(new CustomNameInfo
                {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Shark_Eyes.cs
                    [LanguageCode.English] = DesignerName(typeof(Horse_Eyes)),
========
                    [LanguageCode.English] = DesignerName(typeof(Vigilantest)),
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilantest.cs
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
            // Vanilla 96f
            agent.LOSCone = 180f;
        }
    }
}