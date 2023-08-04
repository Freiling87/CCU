﻿using RogueLibsCore;

namespace CCU.Traits.Cost_Currency
{
    public class Blood_Covenant : T_CostCurrency
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Blood_Covenant>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to blood, which can be paid with blood bags. If the agent has Vampirism, the player has an alternative choice... but they're being a little dodgy about specifics.",
                    [LanguageCode.Spanish] = "Este NPC usa sangre como moneda, sea por bolsas o por (Quotes)Acesso Directo(Quotes) si el NPC tiene vampirismo.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Blood_Covenant)),
                    [LanguageCode.Spanish] = "Sangroso",

                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { DesignerName(typeof(Booze_Bargain)) },
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
