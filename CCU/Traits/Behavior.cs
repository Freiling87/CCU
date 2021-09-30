using BepInEx.Logging;
using CCU.Traits.AI;
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.Behavior;
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
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<Type> BehaviorTraits = new List<Type>()
		{
			typeof(Hire_BreakIn),
			typeof(TraitTrigger_HonorableThief),
			typeof(Vendor_Thief),
		};
		public static List<Type> HireTraits = new List<Type>()
		{
			typeof(Hire_BreakIn),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Hire_BreakIn),
			typeof(Vendor_Thief),
		};
		public static List<Type> VendorTypes = new List<Type>()
		{
			typeof(Vendor_Armorer),
			typeof(Vendor_Assassin),
			typeof(Vendor_Banana),
			typeof(Vendor_Barbarian),
			typeof(Vendor_Bartender),
			typeof(Vendor_BartenderDive),
			typeof(Vendor_BartenderFancy),
			typeof(Vendor_Blacksmith),
			// typeof(Vendor_Buyer), // Exclude
			typeof(Vendor_Cannibal),
			typeof(Vendor_ConsumerElectronics),
			typeof(Vendor_Contraband),
			typeof(Vendor_CopStandard),
			typeof(Vendor_CopSWAT),
			typeof(Vendor_Demolitionist),
			typeof(Vendor_DrugDealer),
			typeof(Vendor_Firefighter),
			typeof(Vendor_Gunsmith),
			typeof(Vendor_HardwareStore),
			typeof(Vendor_HighTech),
			typeof(Vendor_Hypnotist),
			typeof(Vendor_JunkDealer),
			typeof(Vendor_McFuds),
			typeof(Vendor_MedicalSupplier),
			typeof(Vendor_MiningGear),
			typeof(Vendor_MonkeMart),
			typeof(Vendor_MovieTheater),
			typeof(Vendor_Occultist),
			typeof(Vendor_OutdoorOutfitter),
			typeof(Vendor_PawnShop),
			typeof(Vendor_PestControl),
			typeof(Vendor_Pharmacy),
			typeof(Vendor_ResistanceCommissary),
			typeof(Vendor_RiotDepot),
			typeof(Vendor_Scientist),
			typeof(Vendor_Shopkeeper),
			typeof(Vendor_SlaveShop),
			typeof(Vendor_Soldier),
			typeof(Vendor_SportingGoods),
			typeof(Vendor_Teleportationist),
			typeof(Vendor_Thief),
			typeof(Vendor_ToyStore),
			typeof(Vendor_UpperCruster),
			typeof(Vendor_Vampire),
			typeof(Vendor_Villain),
		};

		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			Core.LogMethodCall();

			foreach (Type trait in traitList)
			{
				TraitInfo info = TraitInfo.Get(trait);
				TraitUnlock unlock = RogueLibs.GetUnlock<TraitUnlock>(info.Name);
				string displayText = unlock.GetName();
				logger.LogDebug(displayText);

				if (agent.HasTrait<Trait>())
				{
					logger.LogDebug("Found match: " + displayText);

					return true;
				}
			}

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
