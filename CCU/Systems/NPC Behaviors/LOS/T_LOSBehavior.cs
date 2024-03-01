using BunnyLibs;

namespace CCU.Traits.LOS_Behavior
{
	public abstract class T_LOSBehavior : T_DesignerTrait, ISetupAgentStats, ICheckAgentLOS
	{
		//	ICheckAgentLOS
		public abstract int LOSInterval { get; }
		public abstract float LOSRange { get; }
		public abstract void LOSAction();

		//	ISetupAgentStats
		public bool BypassUnlockChecks => false;
		public virtual void SetupAgent(Agent agent)
		{
			agent.losCheckAtIntervals = true;
		}
	}
}