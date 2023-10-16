using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
	/// <summary>
	/// WARNING: You're going to want to delete this. Just know that when you do, you're going to be stuck having to remove the vestigial trait string. Consider the ease of the override format and ask whether it's worth the trouble.
	/// </summary>

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
					[LanguageCode.Spanish] = "Los trocitos de carne clasicos, no se porque necesitas este rasgo si es el default pero bueno.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Meat_Chunks)),
					[LanguageCode.Spanish] = "Trocitos de Carne",

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