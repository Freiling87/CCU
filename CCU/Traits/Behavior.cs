using CCU.Traits.AI;
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.RoamBehavior;
using CCU.Traits.AI.TraitTrigger;
using CCU.Traits.AI.Vendor;
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
			typeof(Hire_Thief),
			typeof(RoamBehavior_Pickpocket),
			typeof(TraitTrigger_HonorableThief),
			typeof(Vendor_Thief),
		};
		public static List<Type> HireTraits = new List<Type>()
		{
			typeof(Hire_Thief),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Hire_Thief),
			typeof(Vendor_Thief),
		};
		public static List<Type> NoninteractionTraits = new List<Type>()
		{
			typeof(RoamBehavior_Pickpocket),
			typeof(TraitTrigger_HonorableThief),
		};
		public static List<Type> VendorTraits = new List<Type>()
		{
			typeof(Vendor_Thief),
		};

		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			foreach (Type trait in traitList)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
	}
}
