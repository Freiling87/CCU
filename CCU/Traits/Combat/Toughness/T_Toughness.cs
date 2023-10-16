using BunnyLibs;

namespace CCU.Traits.CombatGeneric
{
	public abstract class T_Toughness : T_CCU, ISetupAgentStats
	{
		public abstract int Toughness { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modToughness = Toughness;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
