namespace CCU.Traits.Combat
{
	internal abstract class T_Toughness : T_CCU, ISetupAgentStats
	{
		internal abstract int Toughness { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modToughness = Toughness;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
