using BunnyLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Systems.Custom_Mugging
{
	internal class T_Mugging : ISetupAgentStats
	{
		public bool BypassUnlockChecks => false;

		// AgentInteractions.DetermineButtons
		// Relationships.ProtectOwnedLight, look for agent.hasMugged.
		//	 Do this outside the vanilla loop.

		public void SetupAgent(Agent agent)
		{
			agent.hasMugged = false;
		}
	}
}