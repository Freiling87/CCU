namespace CCU.Traits.Combat
{
	internal abstract class T_GunSkill : T_CCU, ISetupAgentStats
	{
		internal abstract int GunSkill { get; }

		public void SetupAgentStats(Agent agent)
		{
			agent.modGunSkill = GunSkill;
		}

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
