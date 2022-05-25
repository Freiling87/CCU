using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Riot_Inc : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Riot_Inc>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells anything a rioter could want."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Riot_Inc), "Riot, Inc."),
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
