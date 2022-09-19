using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Eat_Corpses : T_Behavior, ISetupAgentStats
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Eat_Corpses>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will eat corpses like the Cannibal.\n\n<color=red>Requires:</color> {0}", vSpecialAbility.Cannibalize),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Eat_Corpses)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { DesignerName(typeof(Pick_Pockets)), DesignerName(typeof(Suck_Blood)) },
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
            agent.losCheckAtIntervals = true;
        }
    }
}
