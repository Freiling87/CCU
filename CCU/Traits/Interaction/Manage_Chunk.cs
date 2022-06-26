using CCU.Traits.Loadout;
using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
    public class Manage_Chunk : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => "";
        public override bool ExtraTextCostOnly => false;
        public override string InteractionCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Manage_Chunk>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will do Clerk/Jock behaviors if they're placed in certain chunks:\n" +
                    "- Arena\n" +
                    "- Deportation Center\n" +
                    "- Hotel *\n\n" +
                    "*<color=red>Requires</color>: {0}", ShortNameDocumentationOnly(typeof(Manager_Key))),
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
