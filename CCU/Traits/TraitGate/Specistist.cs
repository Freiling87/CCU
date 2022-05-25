using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Specistist : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Specistist>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC is problematically intolerant of Specists' beliefs, and gives an XP bonus if they neutralize them."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Specistist)),
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
