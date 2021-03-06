using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
    public class Meat_Chunks : T_GibType
    {
        public override string audioClipName => VanillaAudio.AgentGib;
        public override DecalSpriteName gibDecal => DecalSpriteName.BloodExplosion;
        public override int gibQuantity => 5;
        public override int gibSpriteIteratorLimit => 5;
        public override GibSpriteNameStem gibType => GibSpriteNameStem.GibletNormal;
        public override string particleEffect => "BloodExplosion";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Meat_Chunks>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is made of meat. Delicious meat."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Meat_Chunks)),
                    
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