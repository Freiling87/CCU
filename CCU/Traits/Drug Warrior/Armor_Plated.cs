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
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Armor_Plated), "Armor-Plated"),
                    
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
