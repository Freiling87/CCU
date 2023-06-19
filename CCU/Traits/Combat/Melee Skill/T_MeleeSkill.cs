namespace CCU.Traits.Combat
{
	internal abstract class T_MeleeSkill : T_CCU, ISetupAgentStats
	{
		internal abstract int MeleeSkill { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modMeleeSkill = MeleeSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
