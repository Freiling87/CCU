using RogueLibsCore;

namespace CCU.Traits.Behavior
{
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Raccoon_Eyes.cs
    public class Owl_Eyes : T_Senses, ISetupAgentStats
========
	public class Vigilant : T_Behavior, ISetupAgentStats
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilant.cs
    {
		public override bool LosCheck => false;
		public override string[] GrabItemCategories => new string[] { };

		[RLSetup]
        public static void Setup()
        {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Raccoon_Eyes.cs
            PostProcess = RogueLibs.CreateCustomTrait<Owl_Eyes>()
========
            PostProcess = RogueLibs.CreateCustomTrait<Vigilant>()
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilant.cs
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sets vision cone to 120 degrees (vanilla value = 96).",
                })
                .WithName(new CustomNameInfo
                {
<<<<<<<< HEAD:CCU/Traits/Senses/Vision/Cone/Raccoon_Eyes.cs
                    [LanguageCode.English] = DesignerName(typeof(Owl_Eyes)),
========
                    [LanguageCode.English] = DesignerName(typeof(Vigilant)),
>>>>>>>> Merge_stash:CCU/Traits/Behavior/Vigilance/Vigilant.cs
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
            agent.LOSCone = 120f;
        }
    }
}