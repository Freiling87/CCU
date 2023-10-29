using CCU.Traits.Trait_Gate;
using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	// Don't rename this: needs to be distinct from Hire trait name
	public class Pick_Pockets : T_LOSBehavior
	{
		public override void SetupAgent(Agent agent)
		{
			if (gc.percentChance(50))
				agent.losCheckAtIntervals = true;
		}

		//	ICheckAgentLOS
		public override int LOSInterval => 9;
		public override float LOSRange => 4f;
		public override void LOSAction()
		{
			if (Owner.specialAbility == VanillaAbilities.StickyGlove // Won't work without SA
				&& !Owner.brainUpdate.thiefNoSteal // Mutator only
				&& !Owner.hasEmployer)  // Poorly-Behaved
			{
				bool honorableThief = Owner.HasTrait<Honorable_Thief>();

				foreach (Agent targetAgent in Owner.losCheckAtIntervalsList)
				{
					Relationship relationship = Owner.relationships.GetRelationship(targetAgent);
					bool honorFlag = honorableThief && (targetAgent.statusEffects.hasTrait(VanillaTraits.HonorAmongThieves) || targetAgent.statusEffects.hasTrait("HonorAmongThieves2"));

					if (relationship.distance < LOSRange
						&& !honorFlag
						&& !targetAgent.mechEmpty && !targetAgent.objectAgent
						&& (relationship.relTypeCode == relStatus.Neutral || relationship.relTypeCode == relStatus.Annoyed || relationship.relTypeCode == relStatus.Hostile) // Added Hostile
						&& Owner.slaveOwners.Count == 0 // Poorly-Behaved
						&& Owner.prisoner == targetAgent.prisoner
						&& (Owner.prisoner <= 0 || Owner.curTileData.chunkID == targetAgent.curTileData.chunkID)
						//&& !targetAgent.hasGettingArrestedByAgent  // Let's test this, it'd be hilarious
						&& !targetAgent.invisible
						//&& !Owner.hectoredAgents.Contains(targetAgent.agentID) // Fuck it man, target them twice!
						&& !GC.tileInfo.DifferentLockdownZones(Owner.curTileData, targetAgent.curTileData))
					{
						Owner.SetDefaultGoal(VAgentGoal.Steal);
						Owner.SetStealingFromAgent(targetAgent);
						Owner.hectoredAgents.Add(targetAgent.agentID);
						Owner.losCheckAtIntervalsTime = -100;
						Owner.noEnforcerAlert = true;
						Owner.oma.mustBeGuilty = true;
						break;
					}
				}
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pick_Pockets>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will pickpocket like the Thief.\n\n<color=red>Requires:</color> {0}", VanillaAbilities.StickyGlove),
					[LanguageCode.Spanish] = string.Format("Este NPC le robara a otros como un Ladron.\n\n<color=red>Requires:</color> {0}", VanillaAbilities.StickyGlove),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pick_Pockets)),
					[LanguageCode.Spanish] = "Carterista",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
	}
}