using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Punch_Drunk_2 : T_PlayerTrait, IModMeleeAttack
	{

		public float MeleeDamage => 1.00f;
		public float MeleeKnockback => 1.00f;
		public float MeleeLunge => 1.00f;
		public float MeleeSpeed => 1.30f;
		public bool ApplyModMeleeAttack() => (Owner.agentInvDatabase.equippedWeapon.invItemName == VItemName.Fist);
		public bool CanHitGhost() => false;
		public bool MobilityMandatory() => false;
		public bool MobilityProhibited() => false;
		public void OnStrike(PlayfieldObject target) { }
		public bool? SetMobility() => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Punch_Drunk_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Unarmed attack speed increased by 30%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Punch_Drunk_2)),
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = true,
					
					UnlockCost = 10,
					Unlock =
					{
						categories = { CTraitCategory.Unarmed },
						isUpgrade = true,
					}
				});
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}