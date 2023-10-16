using BunnyLibs;

namespace CCU.Traits.CombatGeneric
{
	public abstract class T_GunSkill : T_CCU, ISetupAgentStats
	{
		public abstract int GunSkill { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modGunSkill = GunSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
