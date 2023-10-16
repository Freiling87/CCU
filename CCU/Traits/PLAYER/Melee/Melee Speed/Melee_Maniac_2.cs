using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Melee_Maniac_2 : T_PlayerTrait, IModMeleeAttack
	{
		public bool ApplyModMeleeAttack() =>
			Owner.agentInvDatabase.equippedWeapon.itemType == VItemType.WeaponMelee
			&& Owner.agentInvDatabase.equippedWeapon.invItemName != VItemName.Fist;
		public bool CanHitGhost() => false;
		public void OnStrike(PlayfieldObject target) { }
		public bool? SetMobility() => null;
		public float MeleeDamage => 1.00f;
		public float MeleeKnockback => 1.00f;
		public float MeleeLunge => 1.00f;
		public float MeleeSpeed => 1.30f; // Highest good value: 1.485 ; Lowest bad value: 1.4875

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Maniac_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 30%.",
					[LanguageCode.Spanish] = "Aumenta la velocidad de ataques Cuerpo a Cuerpo en 30%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Melee_Maniac_2)),
					[LanguageCode.Spanish] = "Violentatico +",
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
						categories = { VTraitCategory.Melee },
					}
				});
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}