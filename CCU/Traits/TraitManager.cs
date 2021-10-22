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
using CCU.Traits.FacialHair;
using CCU.Traits.Relationships;
using CCU.Traits.MapMarker;
using CCU.Traits.Spawn;
using System.IO;
using CCU.Traits.AI.Passive;
using CCU.Traits.AI.Interaction;

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

				list.AddRange(ActiveTraits);
				list.AddRange(PassiveTraits);
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
		public static List<Type> InteractionTraitsGroup
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(HireTypeTraits);
				list.AddRange(InteractionTraits);
				list.AddRange(VendorTypeTraits);

				return list;
			}
		}

		public static List<Type> ActiveTraits = new List<Type>()
		{
			typeof(Behavior_EatCorpse),
			typeof(Behavior_GrabDrugs),
			typeof(Behavior_GrabMoney),
			typeof(Behavior_Pickpocket),
			typeof(Behavior_SuckBlood),
		};
		public static List<Type> PassiveTraits = new List<Type>()
		{
			typeof(Behavior_ExplodeOnDeath),
			typeof(Behavior_Guilty),
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
		public static List<Type> HireCostTraits = new List<Type>()
		{
			typeof(Hire_CostBanana),
			typeof(Hire_CostLess),
			typeof(Hire_CostMore),
		};
		public static List<Type> HireTypeTraits = new List<Type>()
		{
			typeof(Hire_Bodyguard),
			typeof(Hire_BreakIn),
			typeof(Hire_CauseRuckus),
			typeof(Hire_DisarmTrap),
			typeof(Hire_Hack),
			typeof(Hire_Pickpocket),
			typeof(Hire_Poison),
			typeof(Hire_Safecrack),
			typeof(Hire_Tamper),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Interaction_Extortable),
			typeof(Interaction_BuyerAll),
			typeof(Interaction_Moochable),
			typeof(Interaction_BuyerVendor), // TODO: Review this, may have special usage as it's not in Vendor list
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
			typeof(AnnoyedAtSuspicious),
			typeof(Faction_1_Aligned),
			typeof(Faction_1_Hostile),
			typeof(Faction_2_Aligned),
			typeof(Faction_2_Hostile),
			typeof(Faction_3_Aligned),
			typeof(Faction_3_Hostile),
			typeof(Faction_4_Aligned),
			typeof(Faction_4_Hostile),
			typeof(HostileToCannibals),
			typeof(HostileToSoldiers),
			typeof(HostileToVampires),
			typeof(HostileToWerewolves),
		};
		public static List<Type> TraitTriggerTraits = new List<Type>()
		{
			typeof(TraitTrigger_CommonFolk),
			typeof(TraitTrigger_CoolCannibal),
			typeof(TraitTrigger_CopAccess),
			typeof(TraitTrigger_FamilyFriend),
			typeof(TraitTrigger_HonorableThief),
			typeof(TraitTrigger_Scumbag),
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
			List<Type> matchingTraits = traitList.Where(trait => agent.HasTrait(trait)).ToList();

			if (matchingTraits.Count > 1)
			{
				throw new InvalidDataException($"Agent {agent.name} was expected to have one trait from list,"
						+ $"but has {matchingTraits.Count} : [{string.Join(", ", matchingTraits.Select(trait => trait.Name))}]");
			}
			if (matchingTraits.Count == 0)
			{
				throw new InvalidDataException($"Agent {agent.name} was expected to have one trait from list, but has none.");
			}

			// use this one if you expect exactly one match
			return matchingTraits.First();

			// use this one if you expect zero or 1 matches
			return matchingTraits.FirstOrDefault();
		}
		public static bool HasTraitFromList(Agent agent, List<Type> traitList) =>
			traitList.Any(p => agent.HasTrait(p));
		public static void LogTraitList(Agent agent)
		{
			logger.LogDebug("TRAIT LIST: " + agent.agentName);

			foreach (Trait trait in agent.statusEffects.TraitList)
				logger.LogDebug("\t" + trait.traitName);
		}
		internal static List<Trait> OnlyNonhiddenTraits(List<Trait> traitList) =>
			traitList
				.Where(trait => !(AllCCUTraitsGroup.ConvertAll(t => TraitInfo.Get(t).Name).Contains(trait.traitName)))
				.ToList();
	}
}
