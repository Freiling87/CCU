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
                    [LanguageCode.English] = String.Format("On death, this character splashes water everywhere. Contributing factor to death: Uromisotisis poisoning. Don't hold it in, folks."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Water)),
                    
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