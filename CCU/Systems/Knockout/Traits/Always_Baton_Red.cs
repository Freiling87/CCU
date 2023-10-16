using BunnyLibs;
using CCU.Traits;
using RogueLibsCore;

namespace CCU.Systems.Knockout.Traits
{
	public class Always_Baton_Red : T_CCU, IModMeleeAttack
	{
		public bool ApplyModMeleeAttack() => true;
		public bool CanHitGhost() => false;
		public void OnStrike(PlayfieldObject target)
		{
			// If this doesn't work, no need for IModMeleeAttack
		}
		public bool? SetMobility() => null;
		public float MeleeDamage => 1.00f;
		public float MeleeKnockback => 1.00f;
		public float MeleeLunge => 1.00f;
		public float MeleeSpeed => 1.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Always_Baton_Red>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = $"",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Always_Baton_Red)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {
					},
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					IsUnlocked = Core.debugMode,
					UnlockCost = 5,
					Unlock =
					{
						cantLose = true,
						cantSwap = false,
						categories = {
							VTraitCategory.Melee,
						},
						isUpgrade = false,
						upgrade = nameof(Always_Baton_Red_2),
					}
				});
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}