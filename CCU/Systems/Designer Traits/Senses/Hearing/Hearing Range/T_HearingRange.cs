namespace CCU.Traits.Senses.Hearing_Range
{
	internal abstract class T_HearingRange : T_Hearing, ISetupAgentStats
	{
		internal abstract float hearingRange { get; }
		internal abstract bool canHearNoise(Noise noise);

		public void SetupAgentStats(Agent agent)
		{
			agent.hearingRange = hearingRange;
		}
	}
}