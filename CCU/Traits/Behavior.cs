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
		public static List<Type> VendorTypes = new List<Type>()
		{
			typeof(Vendor_Armorer),
			typeof(Vendor_Assassin),
			typeof(Vendor_Banana),
			typeof(Vendor_Barbarian),
			typeof(Vendor_Bartender),
			typeof(Vendor_Blacksmith),
			// typeof(Vendor_Buyer), // Exclude
			typeof(Vendor_ConsumerElectronics),
			typeof(Vendor_CopStandard),
			typeof(Vendor_Demolitionist),
			typeof(Vendor_DrugDealer),
			typeof(Vendor_Firefighter),
			typeof(Vendor_Gunsmith),
			typeof(Vendor_HighTech),
			typeof(Vendor_Hypnotist),
			typeof(Vendor_JunkDealer),
			typeof(Vendor_McFuds),
			typeof(Vendor_MedicalSupplier),
			typeof(Vendor_MiningGear),
			typeof(Vendor_MovieTheater),
			typeof(Vendor_Occultist),
			typeof(Vendor_OutdoorOutfitter),
			typeof(Vendor_PawnShop),
			typeof(Vendor_PestControl),
			typeof(Vendor_ResistanceCommissary),
			typeof(Vendor_RiotDepot),
			typeof(Vendor_Scientist),
			typeof(Vendor_Shopkeeper),
			typeof(Vendor_SlaveShop),
			typeof(Vendor_Soldier),
			typeof(Vendor_SportingGoods),
			typeof(Vendor_Teleportationist),
			typeof(Vendor_Thief),
			typeof(Vendor_HardwareStore),
			typeof(Vendor_UpperCruster),
			typeof(Vendor_Vampire),
			typeof(Vendor_Villain),
		};
		
		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			Core.LogMethodCall();

			foreach (Type trait in traitList)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
		public static Type GetOnlyTraitFromList(Agent agent, List<Type> traitList)
		{
			Core.LogMethodCall();

			foreach (Type trait in traitList)
				if (agent.HasTrait<Trait>())
					return trait;

			return null;
		}
	}
}
