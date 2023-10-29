using BunnyLibs;

namespace CCU.Traits.Combat_
{
	public abstract class T_MeleeSkill : T_CCU, ISetupAgentStats
	{
		public abstract int MeleeSkill { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.modMeleeSkill = MeleeSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
