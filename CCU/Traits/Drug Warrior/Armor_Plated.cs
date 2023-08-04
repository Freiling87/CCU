using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Armor_Plated : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.ResistBullets;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Armor_Plated>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character gains a layer of bullet resistance when entering combat."),
                    [LanguageCode.Spanish] = "Este NPC se vuelve un poquito apruba de balas al entrar en combate.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Armor_Plated), "Armor-Plated"),
                    [LanguageCode.Spanish] = "Chaletado",

                })
                .WithUnlock(new TraitUnlock_CCU
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
