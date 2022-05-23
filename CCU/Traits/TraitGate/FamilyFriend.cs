using RogueLibsCore;
using System;

namespace CCU.Traits.TraitGate
{
    public class FamilyFriend : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<FamilyFriend>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC will be Aligned to a player with Friend of the Family."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.TraitGate_FamilyFriend,
                    [LanguageCode.Russian] = "",
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
