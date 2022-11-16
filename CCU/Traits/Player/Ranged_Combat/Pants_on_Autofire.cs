using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Ranged_Combat
{
    public class Pants_on_Autofire : T_PlayerTrait
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pants_on_Autofire>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("You totally don't have to shit, you *swear*, but you're really in a hurry so you need to shoot all these guys real quick. All your weapons have autofire. Good luck in there."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Pants_on_Autofire)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 5,
                    IsAvailable = true,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 15,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}