using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Punch_Drunk_2 : T_PlayerTrait, IModMeleeAttack
	{

		public float MeleeDamage => throw new System.NotImplementedException();
		public float MeleeKnockback => throw new System.NotImplementedException();
		public float MeleeLunge => throw new System.NotImplementedException();
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
			PostProcess = RogueLibs.CreateCustomTrait<Punch_Drunk_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Unarmed attack speed increased by 30%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Punch_Drunk_2)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
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