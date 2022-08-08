using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Grab_Money : T_Behavior, ISetupAgentStats
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => new string[] { VItemCategory.Money };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Grab_Money>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will grab money if they see it."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Grab_Money)),
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

        public void SetupAgentStats(Agent agent)
        {
            agent.losCheckAtIntervals = true;
        }
    }
}
