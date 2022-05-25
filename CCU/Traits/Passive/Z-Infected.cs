using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Z_Infected : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Z_Infected>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is infected with the Z-Virus. They are not a zombie yet, but will become one when killed."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Z_Infected),("Z-Infected")),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Possessed)) },
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