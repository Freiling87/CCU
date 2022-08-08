using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
    public class Short_Fuse : T_ExplosionTimer
    {
        public override float TimeFactor => 0.66f;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Short_Fuse>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format($"Multiplies explosion timer by 0.66. Multiplier compounds with other Fuse traits. Vanilla fuse is 1.5 seconds."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Short_Fuse)),
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