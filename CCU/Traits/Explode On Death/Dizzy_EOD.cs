using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    /// <summary>
    /// Named distinct from the Status effect
    /// </summary>
    public class Dizzy_EOD : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Dizzy;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Dizzy_EOD>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character makes anyone nearby dizzy. I can't think of any possible reason for this."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Dizzy_EOD), "Dizzy"),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = RogueFramework.Unlocks.OfType<T_ExplodeOnDeath>().Where(c => !(c is Dizzy_EOD)).Select(c => c.TextName).ToList(),
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