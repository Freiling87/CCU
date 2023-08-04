using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Outlaw : T_CCU
    {
        // Pending implementation of Law system
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Outlaw>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Agent ignores any laws applied by Mutators."),
                    [LanguageCode.Spanish] = "Este personaje ignora las leyes impuestas por modificadores.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Outlaw)),
                    [LanguageCode.Spanish] = "Sin Leyes",

                })
                .WithUnlock(new TraitUnlock_CCU
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
