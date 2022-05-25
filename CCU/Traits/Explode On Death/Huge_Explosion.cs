using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
    public class Huge_Explosion : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Huge;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Huge_Explosion>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character explodes. 32 Slaves' worth!!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Huge_Explosion)),
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