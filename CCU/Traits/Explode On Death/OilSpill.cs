using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class OilSpill : T_ExplodeOnDeath
    {
        public override string ExplosionType => CExplosionType.OilSpill;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Ooze>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character splashes flammable oil everywhere. "),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Ooze)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = RogueFramework.Unlocks.OfType<T_ExplodeOnDeath>().Where(c => !(c is Ooze)).Select(c => c.TextName).ToList(),
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