using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
    public class Normal : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Normal;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Normal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character explodes. About 1 Slave's worth."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Normal)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DesignerName(typeof(Big)), DesignerName(typeof(Huge)) },
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