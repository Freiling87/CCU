using BunnyLibs;

namespace CCU.Traits.Combat_
{
	public abstract class T_GunSkill : T_CCU, ISetupAgentStats
	{
		public abstract int GunSkill { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.modGunSkill = GunSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
