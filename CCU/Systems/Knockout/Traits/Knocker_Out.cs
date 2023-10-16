using BunnyLibs;
using System;

namespace CCU.Systems.Knockout.Traits
{
	public class Knocker_Out : T_CCU, IModMeleeAttack
	{
		public float MeleeDamage => throw new NotImplementedException();
		public float MeleeKnockback => throw new NotImplementedException();
		public float MeleeLunge => throw new NotImplementedException();
		public float MeleeSpeed => throw new NotImplementedException();
		public bool ApplyModMeleeAttack() => Owner.inventory.equippedWeapon.invItemName == VItemName.Fist;

		public bool CanHitGhost()
		{
			throw new NotImplementedException();
		}

		public bool MobilityMandatory() => false;
		public bool MobilityProhibited() => false;

		public void OnStrike(PlayfieldObject target)
		{
			throw new NotImplementedException();
		}

		public bool? SetMobility()
		{
			throw new NotImplementedException();
		}
	}
}
