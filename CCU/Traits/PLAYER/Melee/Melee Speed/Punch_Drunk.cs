using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Punch_Drunk : T_PlayerTrait, IModMeleeAttack
	{
		public float MeleeDamage => throw new System.NotImplementedException();
		public float MeleeKnockback => throw new System.NotImplementedException();
		public float MeleeLunge => throw new System.NotImplementedException();
		public float MeleeSpeed => 1.15f;
		public bool ApplyModMeleeAttack() => Owner.agentInvDatabase.equippedWeapon.invItemName == VItemName.Fist;
		public bool CanHitGhost() => false;
		public bool MobilityMandatory() => false;
		public bool MobilityProhibited() => false;
		public void OnStrike(PlayfieldObject target) { }
		public bool? SetMobility() => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Punch_Drunk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Unarmed attack speed increased by 15%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Punch_Drunk)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 5,
					Upgrade = nameof(Punch_Drunk_2),
					Unlock =
					{
						categories = { CTraitCategory.Unarmed },
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}