using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
    public class Leaves : T_GibType
    {
        public override string audioClipName => VanillaAudio.BushDestroy;
        public override DecalSpriteName gibDecal => DecalSpriteName.SlimePuddle;
        public override int gibQuantity => 8;
        public override int gibSpriteIteratorLimit => 5;
        public override GibSpriteNameStem gibType => GibSpriteNameStem.BushWreckage;
        public override string particleEffect => null; // "SlimeExplosion";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Leaves>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is made of plant. Un-delicious plant."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Leaves)),
                    
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