using CCU.Localization;
using Google2u;
using RogueLibsCore;
using System.Linq;

namespace CCU.Extensions
{
    public static class E_Agent
    {
		public static bool IsEnforcer(this Agent agent) =>
			agent.enforcer ||
			agent.HasTrait(nameof(StatusEffectNameDB.rowIds.TheLaw)) ||
			CAgentGroup.LawEnforcers.Contains(agent.agentName);

		public static bool IsCriminal(this Agent agent) =>
			CAgentGroup.Criminals.Contains(agent.agentName);
	}
}