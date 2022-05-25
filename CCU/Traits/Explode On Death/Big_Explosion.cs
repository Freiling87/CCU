using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class Big_Explosion : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Big;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Big_Explosion>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character explodes. About 4 Slaves' worth."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Big_Explosion)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = RogueFramework.Unlocks.OfType<T_ExplodeOnDeath>().Where(c => !(c is Big_Explosion)).Select(c => c.TextName).ToList(),
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