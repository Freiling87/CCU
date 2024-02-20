using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Melee_Maniac : T_PlayerTrait, IModMeleeAttack
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
		public float MeleeSpeed => 1.15f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Melee_Maniac>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 15%.",
					[LanguageCode.Spanish] = "Aumenta la velocidad de ataques cuerpo a cuerpo por 15%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Melee_Maniac)),
					[LanguageCode.Spanish] = "Violentatico",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = Core.designerEdition,
					
					UnlockCost = 5,
					Upgrade = nameof(Melee_Maniac_2),
					Unlock =
					{
						categories = { VTraitCategory.Melee },
					}
				});
		}
		
		
	}
}