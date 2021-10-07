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
using CCU.Traits.AI.Interaction;
using CCU.Traits.FacialHair;

namespace CCU.Traits
{
	public static class TraitManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<Type> AppearanceTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustcheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair),
		};
		public static List<Type> FacialHairTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustcheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair),
		};
		public static List<Type> HiddenTraits
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(HireTraits);
				list.AddRange(InteractionTraits);
				list.AddRange(LOSTraits);
				list.AddRange(VendorTypes);

				return list;
			}
		}
		public static List<string> HiddenTraitNames
		{
			get
			{
				return HiddenTraits.ConvertAll(t => TraitInfo.Get(t).Name);
			}
		}
		public static List<Type> HireTraits = new List<Type>() // Excludes cost modification traits
		{
			typeof(Hire_Bodyguard),
			typeof(Hire_BreakIn),
			typeof(Hire_CauseRuckus),
			typeof(Hire_Hack),
			typeof(Hire_Safecrack),
			typeof(Hire_Tamper),
		};
		public static List<Type> InteractionTraits = new List<Type>() // Technically Vendors are a subtype of this so treat accordingly
		{
			typeof(Interaction_Fence),
			typeof(Interaction_Moochable),
			typeof(Interaction_VendorBuyer), // TODO: Review this, may have special usage as it's not in Vendor list
		};
		public static List<Type> LoadoutTraits = new List<Type>()
		{

		};
		public static List<Type> LOSTraits = new List<Type>()
		{
			typeof(Behavior_EatCorpse),
			typeof(Behavior_GrabDrugs),
			typeof(Behavior_GrabMoney),
			typeof(Behavior_Pickpocket),
			typeof(Behavior_SuckBlood),
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
			typeof(Vendor_Cannibal),
			typeof(Vendor_ConsumerElectronics),
			typeof(Vendor_Contraband),
			typeof(Vendor_CopStandard),
			typeof(Vendor_CopSWAT),
			typeof(Vendor_Demolitionist),
			typeof(Vendor_DrugDealer),
			typeof(Vendor_Firefighter),
			typeof(Vendor_FireSale),
			typeof(Vendor_Gunsmith),
			typeof(Vendor_HardwareStore),
			typeof(Vendor_HighTech),
			typeof(Vendor_HomeFortressOutlet),
			typeof(Vendor_Hypnotist),
			typeof(Vendor_JunkDealer),
			typeof(Vendor_McFuds),
			typeof(Vendor_MedicalSupplier),
			typeof(Vendor_MiningGear),
			typeof(Vendor_MonkeMart),
			typeof(Vendor_MovieTheater),
			typeof(Vendor_Occultist),
			typeof(Vendor_OutdoorOutfitter),
			typeof(Vendor_PacifistProvisioner),
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
			typeof(Vendor_ThiefMaster),
			typeof(Vendor_ThrowceryStore),
			typeof(Vendor_ToyStore),
			typeof(Vendor_UpperCruster),
			typeof(Vendor_Vampire),
			typeof(Vendor_Villain),
		};

		public static Type GetOnlyTraitFromList(Agent agent, List<Type> traitList)
		{
			if (traitList.Count() == 0)
				return null;

			Core.LogMethodCall();

			foreach (Type trait in traitList)
			{
				TraitInfo info = TraitInfo.Get(trait);
				TraitUnlock unlock = RogueLibs.GetUnlock<TraitUnlock>(info.Name);
				string displayText = unlock.GetName();
				logger.LogDebug(displayText);

				if (agent.HasTrait(displayText))
				{
					logger.LogDebug("Found match: " + displayText);

					return trait;
				}
			}

			return null;
		}
		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			Core.LogMethodCall();

			foreach (Type trait in traitList)
			{
				TraitInfo info = TraitInfo.Get(trait);
				TraitUnlock unlock = RogueLibs.GetUnlock<TraitUnlock>(info.Name);
				string displayText = unlock.GetName();
				logger.LogDebug(displayText);

				if (agent.HasTrait(trait))
				{
					logger.LogDebug("Found match: " + displayText);

					return true;
				}
			}

			return false;
		}
		public static void LogTraitList(Agent agent)
		{
			logger.LogDebug("TRAIT LIST: " + agent.agentName);

			foreach (Trait trait in agent.statusEffects.TraitList)
				logger.LogDebug("\t" + trait.traitName);
		}
		internal static List<Trait> OnlyNonhiddenTraits(List<Trait> traitList)
		{
			List<string> traitNames = HiddenTraits.ConvertAll(t => TraitInfo.Get(t).Name);

			return traitList
				.Where(trait => !(traitNames.Contains(trait.traitName)))
				.ToList();
		}
	}
}
