using CCU.Traits.Hire_Type;
using CCU.Traits.Merchant_Type;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Localization
{
    public static class Legacy
	{
		public static Dictionary<string, string> TraitConversions = new Dictionary<string, string>()
		{
			{ VanillaAgents.Hacker, nameof(Cyber_Intruder) },
			{ VanillaAgents.Shopkeeper, nameof(General_Store) },
			{ VanillaAgents.Soldier, nameof(Muscle) },
			{ VanillaAgents.Thief, nameof(Intruders_Warehouse) },
			{ VanillaAgents.Vampire, nameof(Bloodsucker_Bazaar) }
		};
	}
}