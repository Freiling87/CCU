using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Knockback_Peon : T_PlayerTrait, IModMeleeAttack
	{
		public bool ApplyModMeleeAttack() => true;
		public bool CanHitGhost() => false;
		public void OnStrike(PlayfieldObject target) { }
		public bool? SetMobility() => null;
		public float MeleeDamage => 1.00f;
		public float MeleeKnockback => 0.90f;
		public float MeleeLunge => 1.00f;
		public float MeleeSpeed => 1.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Knockback_Peon>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Cause people you hit to be knocked back less. Makes followup attacks easier."),
					[LanguageCode.Spanish] = "Reduce el retroceso de tus golpes, facilitando combos rapidos.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Knockback_Peon)),
					[LanguageCode.Spanish] = "Peon del Retroceso",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {
						VanillaTraits.KnockbackKing,
						VanillaTraits.WallsWorstNightmare
					},
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
					Unlock =
					{
						cantLose = false,
						cantSwap = false,
						categories = {
							VTraitCategory.Melee,
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