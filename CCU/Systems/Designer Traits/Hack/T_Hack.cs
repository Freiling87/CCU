using BunnyLibs;

namespace CCU.Traits.Hack
{
	public abstract class T_Hack : T_CCU, ISetupAgentStats
	{
		public T_Hack() : base() { }

		public abstract string ButtonText { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.hackable = true;
		}
	}
}