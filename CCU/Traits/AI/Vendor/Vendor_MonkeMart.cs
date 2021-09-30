using RogueLibsCore;
using System;

namespace CCU.Traits.AI.Vendor
{
    public class Vendor_MonkeMart : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Vendor_MonkeMart>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells stuff for monke, gorgia, you name it."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AI_Vendor_MonkeMart,
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
