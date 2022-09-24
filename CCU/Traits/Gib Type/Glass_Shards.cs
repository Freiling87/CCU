using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
    public class Glass_Shards : T_GibType
    {
        public override string audioClipName => VanillaAudio.WallDestroyGlass;
        public override DecalSpriteName gibDecal => DecalSpriteName.None;
        public override int gibQuantity => 12;
        public override int gibSpriteIteratorLimit => 5;
        public override GibSpriteNameStem gibType => GibSpriteNameStem.WindowWreckage;
        public override string particleEffect => "ObjectDestroyedSmoke";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Glass_Shards>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is some kind of crystalline lifeform. More of a carbon-based guy myself, but I respect their lifestyle choice of being born that way."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Glass_Shards)),
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