using BunnyLibs;

namespace CCU.Traits.CombatGeneric
{
	public abstract class T_MeleeSkill : T_CCU, ISetupAgentStats
	{
		public abstract int MeleeSkill { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modMeleeSkill = MeleeSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
