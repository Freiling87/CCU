using RogueLibsCore;
using System;

namespace CCU.Traits.AI.Behavior
{
    public class Behavior_ExplodeOnDeath : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Behavior_ExplodeOnDeath>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character explodes when killed."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AI_Behavior_ExplodeOnDeath,
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