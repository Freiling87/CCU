using BunnyLibs;

namespace CCU.Traits.Combat_
{
	public abstract class T_MeleeSkill : T_DesignerTrait, ISetupAgentStats
	{
		public abstract int MeleeSkill { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.modMeleeSkill = MeleeSkill;
		}

		
		
	}
}
