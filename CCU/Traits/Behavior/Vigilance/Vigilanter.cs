using RogueLibsCore;

namespace CCU.Traits.Behavior
{
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Falcon_Eyes.cs
    public class Falcon_Eyes : T_Senses, ISetupAgentStats
========
	public class Vigilanter : T_Behavior, ISetupAgentStats
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilanter.cs
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => new string[] { };

        [RLSetup]
        public static void Setup()
        {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Falcon_Eyes.cs
            PostProcess = RogueLibs.CreateCustomTrait<Falcon_Eyes>()
========
            PostProcess = RogueLibs.CreateCustomTrait<Vigilanter>()
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilanter.cs
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sets vision cone to 30 degrees (vanilla value = 96).",
                })
                .WithName(new CustomNameInfo
                {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Falcon_Eyes.cs
                    [LanguageCode.English] = DesignerName(typeof(Falcon_Eyes)),
========
                    [LanguageCode.English] = DesignerName(typeof(Vigilanter)),
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilanter.cs
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
            agent.LOSCone = 30f;
        }
    }
}