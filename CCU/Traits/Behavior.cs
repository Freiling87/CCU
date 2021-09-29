using CCU.Traits.AI;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits
{
	public static class Behavior
	{
		public static List<Type> BehaviorTraits = new List<Type>()
		{
			typeof(Thief_Hire),
			typeof(Thief_HonestThief),
			typeof(Thief_Pickpocket),
			typeof(Thief_Vendor),
		};
		public static List<Type> HireTraits = new List<Type>()
		{
			typeof(Thief_Hire),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Thief_Hire),
			typeof(Thief_Vendor),
		};
		public static List<Type> NoninteractionTraits = new List<Type>()
		{
			typeof(Thief_HonestThief),
			typeof(Thief_Pickpocket),
		};
		public static List<Type> VendorTraits = new List<Type>()
		{
			typeof(Thief_Vendor),
		};

		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			Core.LogMethodCall();

			foreach (Type trait in traitList)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
	}
}
