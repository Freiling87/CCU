using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
    public class Gibless : T_GibType
    {
        public override string audioClipName => null;
        public override DecalSpriteName gibDecal => DecalSpriteName.None;
        public override int gibQuantity => 0;
        public override int gibSpriteIteratorLimit => 0;
        public override GibSpriteNameStem gibType => GibSpriteNameStem.GibletNone;
        public override string particleEffect => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Gibless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("You should count your blessings. Not everyone is so lucky as to explode into meat chunks when they die. Those poor Cop Bots, there's nothing left for the family to bury."),
                    [LanguageCode.Spanish] = "El cuerpo de este NPC es solo un plato de aire con un lado de oxigeno, justo como los de que mi madre preparaba...",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Gibless)),
                    [LanguageCode.Spanish] = "Sin Trocitos",
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