using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Seek_and_Destroy : T_Behavior
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Seek_and_Destroy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will follow and attack the player like the Killer Robot."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Seek_and_Destroy), "Seek & Destroy"),
                    
                })
                .WithUnlock(new TraitUnlock
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

        public static bool IsVanillaKillerRobot(Agent agent) =>
            agent.killerRobot &&
            agent.agentName == VanillaAgents.KillerRobot;
    }
}