using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
	public class Ectoplasm : T_GibType
	{
		public override string audioClipName => VanillaAudio.AgentGib;
		public override DecalSpriteName gibDecal => DecalSpriteName.BloodExplosionGhost;
		public override int gibQuantity => 5;
		public override int gibSpriteIteratorLimit => 5;
		public override GibSpriteNameStem gibType => GibSpriteNameStem.GibletGhost;
		public override string particleEffect => "BloodExplosionGhost";


		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ectoplasm>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character's body explodes into chunks of... ghost? Probably not vegan, if you were wondering."),
					[LanguageCode.Spanish] = "El cuerpo de este NPC esta echo de pedacitos de fantasma, los cuales van bien con un poco de ensalada y una soda.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ectoplasm)),

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