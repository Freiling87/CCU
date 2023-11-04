using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Punch_Drunk : T_PlayerTrait, IModMeleeAttack
	{
		public float MeleeDamage => 1.00f;
		public float MeleeKnockback => 1.00f;
		public float MeleeLunge => 1.00f;
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
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Punch_Drunk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Unarmed attack speed increased by 15%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Punch_Drunk)),
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 5,
					Upgrade = nameof(Punch_Drunk_2),
					Unlock =
					{
						categories = { CTraitCategory.Unarmed },
					}
				});
		}
		
		
	}
}