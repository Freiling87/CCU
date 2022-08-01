using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Concealed_Carrier : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Concealed_Carrier>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent knows their rights, and declines your request to shut up about it. They'll hide their weapon when not in combat."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Concealed_Carrier)),
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
    }
}