// Add rapid fire

using BunnyLibs;
using CCU.Traits;
using RogueLibsCore;

namespace CCU.Systems.Knockout.Traits
{
	public class Always_Baton_Red_2 : T_CCU, IModMeleeAttack
	{
		public float MeleeDamage => throw new System.NotImplementedException();
		public float MeleeKnockback => throw new System.NotImplementedException();
		public float MeleeLunge => throw new System.NotImplementedException();
		public float MeleeSpeed => throw new System.NotImplementedException();

		public bool ApplyModMeleeAttack()
		{
			throw new System.NotImplementedException();
		}

		public bool CanHitGhost()
		{
			throw new System.NotImplementedException();
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }

		public void OnStrike()
		{
			throw new System.NotImplementedException();
		}

		public bool? SetMobility()
		{
			throw new System.NotImplementedException();
		}

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Always_Baton_Red_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = $"",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Always_Baton_Red_2)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {
					},
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = false,
					IsPlayerTrait = true,
					IsUnlocked = Core.debugMode,
					UnlockCost = 10,
					Unlock =
					{
					cantLose = true,
					cantSwap = true,
					categories = {
						VTraitCategory.Melee,
					},
					isUpgrade = true,
					upgrade = null,
					}
				});
		}

		public void OnStrike(PlayfieldObject target)
		{
			throw new System.NotImplementedException();
		}
	}
}