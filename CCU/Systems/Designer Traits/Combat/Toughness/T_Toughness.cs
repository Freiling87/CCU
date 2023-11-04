using BunnyLibs;

namespace CCU.Traits.Combat_
{
	public abstract class T_Toughness : T_DesignerTrait, ISetupAgentStats
	{
		public abstract int Toughness { get; }

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.modToughness = Toughness;
		}
	}
}
