using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Immovable : T_PlayerTrait, IModResistances
	{
		public float ResistKnockback => 99.00f;
		public float ResistBullets => 1.0f;
		public float ResistExplosion => 1.0f;
		public float ResistFire => 1.0f;
		public float ResistMelee => 1.0f;
		public float ResistPoison => 1.0f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Immovable>()
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
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = {
						VanillaTraits.SkinnyNerdlinger,
						"KnockbackLess",
					},
					CharacterCreationCost = 7,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsUnlocked = Core.debugMode,
					UnlockCost = 15,
					Unlock =
					{
						cantLose = false,
						cantSwap = false,
						categories = {
							VTraitCategory.Defense,
							VTraitCategory.Movement,
						},
						isUpgrade = false,
						upgrade = null,
					}
				});
		}
		
		
	}
}