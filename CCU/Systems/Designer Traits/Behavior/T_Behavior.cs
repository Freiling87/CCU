using BunnyLibs;

namespace CCU.Traits.Behavior
{
	public abstract class T_Behavior : T_DesignerTrait, ISetupAgentStats
	{
		public T_Behavior() : base() { }

		//	ISetupAgentStats
		public bool BypassUnlockChecks => false;
		public abstract void SetupAgent(Agent agent);
	}
}