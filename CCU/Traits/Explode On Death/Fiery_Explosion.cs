using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
    public class Fiery_Explosion : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Molotov;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Fiery_Explosion>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character explodes like the Molotovs your mother used to make!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Fiery_Explosion)),
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