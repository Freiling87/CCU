using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
	public class Ice_Shards : T_GibType
	{
		public override string audioClipName => VanillaAudio.IceBreak;
		public override DecalSpriteName gibDecal => DecalSpriteName.None;
		public override int gibQuantity => 5;
		public override int gibSpriteIteratorLimit => 2;
		public override GibSpriteNameStem gibType => GibSpriteNameStem.GibletIce;
		public override string particleEffect => "ObjectDestroyedSmoke";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ice_Shards>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character's body explodes into shards of ice."),
					[LanguageCode.Spanish] = "Este NPC esta construido con hielo super convincente! casi parece tener emociones!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ice_Shards)),
					[LanguageCode.Spanish] = "Hielo",

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