using BepInEx.Logging;
using CCU.Traits.AI;
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.Active;
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
using CCU.Traits.Relationships;
using CCU.Traits.MapMarker;
using CCU.Traits.Spawn;

namespace CCU.Traits
{
	public static class TraitManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<Type> AllCCUTraitsGroup
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(AppearanceTraitsGroup);

				list.AddRange(BehaviorActiveTraits);
				list.AddRange(BehaviorNonLOSTraits);
				list.AddRange(BodyguardedTraits);
				list.AddRange(HireCostTraits);
				list.AddRange(HireTypeTraits);
				list.AddRange(InteractionTraits);
				list.AddRange(LoadoutTraits);
				list.AddRange(RelationshipTraits);
				list.AddRange(TraitTriggerTraits);
				list.AddRange(VendorTypeTraits);

				return list;
			}
		}
		public static List<string> AllCCUTraitNamesGroup
		{
			get
			{
				return AllCCUTraitsGroup.ConvertAll(t => TraitInfo.Get(t).Name);
			}
		}
		public static List<Type> AppearanceTraitsGroup
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(FacialHairTraits);

				return list;
			}
		}

		public static List<Type> BehaviorActiveTraits = new List<Type>()
		{
			typeof(Behavior_EatCorpse),
			typeof(Behavior_GrabDrugs),
			typeof(Behavior_GrabMoney),
			typeof(Behavior_Pickpocket),
			typeof(Behavior_SuckBlood),
		};
		public static List<Type> BehaviorNonLOSTraits = new List<Type>()
		{
		};
		public static List<Type> BodyguardedTraits = new List<Type>()
		{
			typeof(Bodyguarded_Pilot),
		};
		public static List<Type> FacialHairTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustcheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair),
		};
		public static List<Type> HireCostTraits = new List<Type>() // Excludes cost modification traits
		{
			typeof(Hire_CostBanana),
			typeof(Hire_CostLess),
			typeof(Hire_CostMore),
		};
		public static List<Type> HireTypeTraits = new List<Type>() // Excludes cost modification traits
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
			typeof(Interaction_Extortable),
			typeof(Buyer_All),
			typeof(Interaction_Moochable),
			typeof(Interaction_VendorBuyer), // TODO: Review this, may have special usage as it's not in Vendor list
		};
		public static List<Type> LoadoutTraits = new List<Type>()
		{

		};
		public static List<Type> MapMarkerTraits = new List<Type>()
		{
			typeof(MapMarker_Pilot),
		};
		public static List<Type> RelationshipTraits = new List<Type>()
		{
			typeof(Faction_1_Aligned),
			typeof(Faction_1_Hostile),
			typeof(Faction_2_Aligned),
			typeof(Faction_2_Hostile),
			typeof(Faction_3_Aligned),
			typeof(Faction_3_Hostile),
			typeof(Faction_4_Aligned),
			typeof(Faction_4_Hostile),
		};
		public static List<Type> TraitTriggerTraits = new List<Type>()
		{
			typeof(TraitTrigger_CommonFolk),
			typeof(TraitTrigger_CoolCannibal),
			typeof(TraitTrigger_CopAccess),
			typeof(TraitTrigger_HonorableThief),
		};
		public static List<Type> VendorTypeTraits = new List<Type>()
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
			Core.LogMethodCall();

			if (traitList.Count() == 0)
				return null;

			foreach (Type trait in traitList)
			{
				TraitInfo info = TraitInfo.Get(trait);
				TraitUnlock unlock = RogueLibs.GetUnlock<TraitUnlock>(info.Name);
				string displayText = unlock.GetName();

				if (agent.HasTrait(displayText))
					return trait;
			}

			logger.LogDebug("\tnull return");
			return null;
		}
		public static bool HasTraitFromList(Agent agent, List<Type> traitList)
		{
			foreach (Type trait in traitList)
				if (agent.HasTrait(trait))
					return true;

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
			List<string> traitNames = AllCCUTraitsGroup.ConvertAll(t => TraitInfo.Get(t).Name);

			return traitList
				.Where(trait => !(traitNames.Contains(trait.traitName)))
				.ToList();
		}
	}
}
