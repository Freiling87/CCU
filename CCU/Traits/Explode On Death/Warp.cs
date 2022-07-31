using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class Warp : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.Warp;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Warp>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("All the good ancient curses were taken. This one teleports anyone present for its holder's death to a mildly inconvenient location nearby. OoOoOoOh!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Warp)),
                    
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