using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
    public class Cinematic_Fuse : T_ExplosionTimer
    {
        public override float TimeFactor => 60.0f;

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cinematic_Fuse>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format($"Multiplies explosion timer by 60. Multiplier compounds with other Fuse traits. Vanilla fuse is 1.5 seconds."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cinematic_Fuse)),
                })
                .WithUnlock(new TraitUnlock_CCU
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