using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Not_Vincible : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Not_Vincible>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Go ahead, try to vince them. They simply can't be vinced."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Not_Vincible)),
                })
                .WithUnlock(new TraitUnlock
                {
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