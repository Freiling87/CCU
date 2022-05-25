using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
    public class Ridiculous_Explosion : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Ridiculous;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Ridiculous_Explosion>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character explodes. Over 125 Slaves' worth, a fantastic value!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Ridiculous_Explosion)),
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