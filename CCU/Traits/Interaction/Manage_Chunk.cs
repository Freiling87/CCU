using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
    public class Manage_Chunk : T_Interaction
    {
        public override string ButtonText => "";

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Manage_Chunk>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will do Clerk behaviors if they're placed in certain chunks:\n" +
                    "- Arena\n" +
                    "- Deportation Center\n" +
                    "- Hotel\n"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Manage_Chunk)),
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
