using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
    public class Golemite : T_GibType
    {
        public override string audioClipName => VanillaAudio.WallDestroy;
        public override DecalSpriteName gibDecal => DecalSpriteName.None;
        public override int gibQuantity => 8;
        public override int gibSpriteIteratorLimit => 5;
        public override GibSpriteNameStem gibType => GibSpriteNameStem.FlamingBarrelWreckage;
        public override string particleEffect => "ObjectDestroyedSmoke";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Golemite>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character explodes into chunks of stone when killed. The chunks remain sad about it. Golems have feelings too."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Golemite)),
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