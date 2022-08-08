using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
    public class Shortest_Fuse : T_ExplosionTimer
    {
        public override float TimeFactor => 0.00f;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Shortest_Fuse>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format($"Makes explosion timer instantaneous. Vanilla fuse is 1.5 seconds."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Shortest_Fuse)),
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