using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class Water : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Water;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Water>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character splashes water everywhere. I've heard it's bad to hold it in too long."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Water)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = RogueFramework.Unlocks.OfType<T_ExplodeOnDeath>().Where(c => !(c is Water)).Select(c => c.TextName).ToList(),
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