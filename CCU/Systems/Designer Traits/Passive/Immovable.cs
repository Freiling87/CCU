using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Immovable : T_CCU, IModResistances
	{
		public float ResistKnockback => 0.0f;
		public float ResistBullets => 1.0f;
		public float ResistExplosion => 1.0f;
		public float ResistFire => 1.0f;
		public float ResistMelee => 1.0f;
		public float ResistPoison => 1.0f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Immovable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Immune to Knockback."),
					[LanguageCode.Spanish] = "Este personaje es inmune al retroceso.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Immovable)),
					[LanguageCode.Spanish] = "Inmovible",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = {
						VanillaTraits.SkinnyNerdlinger,
						"KnockbackLess",
					},
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsUnlocked = Core.debugMode,
					UnlockCost = 5,
					Unlock =
					{
						cantLose = false,
						cantSwap = false,
						categories = {
							VTraitCategory.Defense,
						},
						isUpgrade = false,
						upgrade = null,
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}