using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class EMP : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.EMP;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<EMP>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character releases an EMP."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(EMP)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = RogueFramework.Unlocks.OfType<T_ExplodeOnDeath>().Where(c => !(c is EMP)).Select(c => c.TextName).ToList(),
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