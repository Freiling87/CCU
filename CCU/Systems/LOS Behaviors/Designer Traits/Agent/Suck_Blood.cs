using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	public class Suck_Blood : T_LOSBehavior
	{
		public override int LOSInterval => 9;
		public override float LOSRange => 5f;
		public override void LOSAction()
		{
			if (!Owner.hasEmployer  // Poorly-Behaved
				|| Owner.health <= (Owner.healthMax * 0.66f))
			{
				foreach (Agent targetAgent in Owner.losCheckAtIntervalsList)
				{
					Relationship relationship2 = Owner.relationships.GetRelationship(targetAgent);

					if (relationship2.distance < 5f
						&& relationship2.relTypeCode != relStatus.Aligned && relationship2.relTypeCode != relStatus.Loyal && relationship2.relTypeCode != relStatus.Friendly && relationship2.relTypeCode != relStatus.Hostile
						&& targetAgent.agentName != VanillaAgents.Vampire // Needs to be elaborated
						&& !targetAgent.hasGettingBitByAgent // BITER SWARM?
						&& !targetAgent.mechEmpty && !targetAgent.mechFilled && !targetAgent.objectAgent && !targetAgent.dizzy
						&& (targetAgent.localPlayer || targetAgent.isPlayer == 0)
						&& Owner.prisoner == targetAgent.prisoner
						&& !targetAgent.invisible
						&& Owner.slaveOwners.Count == 0  // Poorly-Behaved
						&& (Owner.prisoner <= 0 || Owner.curTileData.chunkID == targetAgent.curTileData.chunkID)
						&& !targetAgent.hasGettingArrestedByAgent
						&& !Owner.hectoredAgents.Contains(targetAgent.agentID)
						&& !GC.tileInfo.DifferentLockdownZones(Owner.curTileData, targetAgent.curTileData)
						&& !targetAgent.dead && !targetAgent.ghost && !targetAgent.hologram && !targetAgent.disappeared && !targetAgent.inhuman && !targetAgent.beast && !targetAgent.zombified)
					{
						Owner.SetPreviousDefaultGoal(Owner.defaultGoal);
						Owner.SetDefaultGoal("Bite");
						Owner.hectoredAgents.Add(targetAgent.agentID);
						Owner.SetBitingTarget(targetAgent);
						Owner.noEnforcerAlert = true;
						Owner.oma.mustBeGuilty = true;
						Owner.oma.hasAttacked = true;
						Owner.losCheckAtIntervalsTime = -100;
						break;
					}
				}
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Suck_Blood>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format
					("This character will suck blood like the Vampire. Agents with Hostile to Vampire will be hostile."),
					[LanguageCode.Spanish] = string.Format
					("Este NPC chupa gente, de manera sangrienta. NPCs y Jugadores que son hostil contra Vampiros tambien seran hostiles a este rasgo."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suck_Blood)),
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
