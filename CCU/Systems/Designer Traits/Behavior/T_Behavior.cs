using BunnyLibs;

namespace CCU.Traits.Behavior
{
	public abstract class T_Behavior : T_CCU, ISetupAgentStats
	{
		public T_Behavior() : base() { }

		//	ISetupAgentStats
		public bool BypassUnlockChecks => false;
		public abstract void SetupAgent(Agent agent);

		public override void OnAdded() { }
		public override void OnRemoved() { }

	}
}