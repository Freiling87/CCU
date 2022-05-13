using BepInEx.Logging;
using CCU.Traits.Hire;
using CCU.Traits.TraitGate;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CCU.Traits.Relationships;
using CCU.Traits.MapMarker;
using CCU.Traits.Spawn;
using System.IO;
using CCU.Traits.Passive;
using CCU.Traits.Active;
using CCU.Traits.Appearance.FacialHair;
using CCU.Traits.MerchantType;
using CCU.Traits.Combat;
using CCU.Traits.Cost;
using CCU.Traits.HireDuration;
using CCU.Traits.Interaction;

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
				list.AddRange(BehaviorPassiveTraits);
				list.AddRange(BodyguardedTraits);
				list.AddRange(CombatTraits);
				list.AddRange(HireCostTraits);
				list.AddRange(HireTypeTraits);
				list.AddRange(InteractionTraits);
				list.AddRange(LoadoutTraits);
				list.AddRange(MapMarkerTraits);
				list.AddRange(MerchantTypeTraits);
				list.AddRange(RelationshipTraits);
				list.AddRange(TraitGateTraits);

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
		public static List<Type> InteractionTraitsGroup
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(HireTypeTraits);
				list.AddRange(InteractionTraits);
				list.AddRange(MerchantTypeTraits);

				return list;
			}
		}

		#region Appearance Trait 
		public static List<Type> AppearanceTraitsGroup
		{
			get
			{
				List<Type> list = new List<Type>();

				list.AddRange(AccessoryTraits);
				list.AddRange(BodyColorTraits);
				list.AddRange(BodyTypeTraits);
				list.AddRange(EyesColorTraits);
				list.AddRange(EyesTypeTraits);
				list.AddRange(FacialHairTraits);
				list.AddRange(HairColorTraits);
				list.AddRange(HairTypeTraits);
				list.AddRange(LegColorTraits);
				list.AddRange(SkinColorTraits);

				return list;
			}
		}
		public static List<Type> AccessoryTraits = new List<Type>()
		{
		};
		public static List<Type> BodyColorTraits = new List<Type>()
		{
		};
		public static List<Type> BodyTypeTraits = new List<Type>()
		{
		};
		public static List<Type> EyesColorTraits = new List<Type>()
		{
		};
		public static List<Type> EyesTypeTraits = new List<Type>()
		{
		};
		public static List<Type> FacialHairTraits = new List<Type>()
		{
			typeof(Beard),
			typeof(Mustache),
			typeof(MustacheCircus),
			typeof(MustacheRedneck),
			typeof(NoFacialHair),
		};
		public static List<Type> HairColorTraits = new List<Type>()
		{
		};
		public static List<Type> HairTypeTraits = new List<Type>()
		{
		};
		public static List<Type> LegColorTraits = new List<Type>()
		{
		};
		public static List<Type> SkinColorTraits = new List<Type>()
		{
		};
#endregion
		public static List<Type> BehaviorActiveTraits = new List<Type>()
		{
			typeof(EatCorpse),
			typeof(GrabDrugs),
			typeof(GrabMoney),
			typeof(Active.Pickpocket),
			typeof(SuckBlood),
		};
		public static List<Type> BehaviorPassiveTraits = new List<Type>()
		{
			typeof(ExplodeOnDeath),
			typeof(Guilty),
		};
		public static List<Type> BodyguardedTraits = new List<Type>()
		{
			typeof(Bodyguarded_Pilot),
		};
		public static List<Type> CombatTraits = new List<Type>()
		{
			typeof(Combat_Coward),
			typeof(Combat_Fearless),
			typeof(Combat_Fearless)
		};
		public static List<Type> HireCostTraits = new List<Type>()
		{
			typeof(CostAlcohol),
			typeof(CostBanana),
			typeof(CostLess),
			typeof(CostMore),
			typeof(CostZero),
		};
		public static List<Type> HireDurationTraits = new List<Type>()
		{
			typeof(HirePermanent),
			typeof(HirePermanentOnly),
		};
		public static List<Type> HireTypeTraits = new List<Type>()
		{
			typeof(Bodyguard),
			typeof(BreakIn),
			typeof(CauseARuckus),
			typeof(DisarmTrap),
			typeof(Hack),
			typeof(Hire.Pickpocket),
			typeof(Poison),
			typeof(Safecrack),
			typeof(Tamper),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Extortable),
			typeof(Moochable),
			typeof(Buyer_MerchantType), // TODO: Review this, may have special usage as it's not in Vendor list
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
		public static List<Type> TraitGateTraits = new List<Type>()
		{
			typeof(TraitGate_CommonFolk),
			typeof(TraitGate_CoolCannibal),
			typeof(TraitGate_CopAccess),
			typeof(TraitGate_FamilyFriend),
			typeof(TraitGate_HonorableThief),
			typeof(TraitGate_Scumbag),
		};
		public static List<Type> MerchantTypeTraits = new List<Type>()
		{
			typeof(MerchantType_Armorer),
			typeof(MerchantType_Assassin),
			typeof(MerchantType_Banana),
			typeof(MerchantType_Barbarian),
			typeof(MerchantType_Bartender),
			typeof(MerchantType_BartenderDive),
			typeof(MerchantType_BartenderFancy),
			typeof(MerchantType_Blacksmith),
			typeof(MerchantType_Cannibal),
			typeof(MerchantType_ConsumerElectronics),
			typeof(MerchantType_Contraband),
			typeof(MerchantType_ConvenienceStore),
			typeof(MerchantType_CopStandard),
			typeof(MerchantType_CopSWAT),
			typeof(MerchantType_Demolitionist),
			typeof(MerchantType_DrugDealer),
			typeof(MerchantType_Firefighter),
			typeof(MerchantType_FireSale),
			typeof(MerchantType_Gunsmith),
			typeof(MerchantType_HardwareStore),
			typeof(MerchantType_HighTech),
			typeof(MerchantType_HomeFortressOutlet),
			typeof(MerchantType_Hypnotist),
			typeof(MerchantType_JunkDealer),
			typeof(MerchantType_McFuds),
			typeof(MerchantType_MedicalSupplier),
			typeof(MerchantType_MiningGear),
			typeof(MerchantType_MonkeMart),
			typeof(MerchantType_MovieTheater),
			typeof(MerchantType_Occultist),
			typeof(MerchantType_OutdoorOutfitter),
			typeof(MerchantType_PacifistProvisioner),
			typeof(MerchantType_PawnShop),
			typeof(MerchantType_PestControl),
			typeof(MerchantType_Pharmacy),
			typeof(MerchantType_ResistanceCommissary),
			typeof(MerchantType_RiotDepot),
			typeof(MerchantType_Scientist),
			typeof(MerchantType_Shopkeeper),
			typeof(MerchantType_SlaveShop),
			typeof(MerchantType_Soldier),
			typeof(MerchantType_SportingGoods),
			typeof(MerchantType_Teleportationist),
			typeof(MerchantType_Thief),
			typeof(MerchantType_ThiefMaster),
			typeof(MerchantType_ThrowceryStore),
			typeof(MerchantType_ToyStore),
			typeof(MerchantType_UpperCruster),
			typeof(MerchantType_Vampire),
			typeof(MerchantType_Villain),
		};

		public static Type GetOnlyTraitFromList(Agent agent, List<Type> traitList)
		{
			List<Type> matchingTraits = traitList.Where(trait => agent.HasTrait(trait)).ToList();

			if (matchingTraits.Count > 1)
				throw new InvalidDataException($"Agent {agent.name} was expected to have one trait from list,"
						+ $"but has {matchingTraits.Count} : [{string.Join(", ", matchingTraits.Select(trait => trait.Name))}]");
			else if (matchingTraits.Count == 0)
				throw new InvalidDataException($"Agent {agent.name} was expected to have one trait from list, but has none.");

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
