using CCU.Traits.Behavior;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Traits.LOS_Behavior
{
	public class Eat_Corpses : T_LOSBehavior
	{
		public override int LOSInterval => 8;
		public override float LOSRange => 5.64f; // Original code uses a square of side 5f, but LOSRange is Euclidean distance
		public override void LOSAction()
		{
			if (!Owner.hasEmployer // Poorly-Behaved
				|| Owner.health <= (Owner.healthMax * 0.66f))
			{
				foreach (Agent corpse in gc.deadAgentList)
				{
					if (corpse.dead && !corpse.resurrect && !corpse.ghost && !corpse.disappeared && !corpse.inhuman && !corpse.cantCannibalize && !corpse.hasGettingBitByAgent && !corpse.KnockedOut()
						&& Owner.prisoner == corpse.prisoner
						&& Owner.slaveOwners.Count == 0 // Poorly-Behaved
						&& (Owner.prisoner <= 0 || Owner.curTileData.chunkID == corpse.curTileData.chunkID)
						&& !corpse.arrested && !corpse.invisible
						&& !GC.tileInfo.DifferentLockdownZones(Owner.curTileData, corpse.curTileData)
						&& Vector2.Distance(Owner.curPosition, corpse.curPosition) <= LOSRange
						&& Owner.movement.HasLOSAgent(corpse)
						&& (corpse.fire == null || Owner.HasTrait<Accident_Prone>()))
					{
						Owner.SetPreviousDefaultGoal(Owner.defaultGoal);
						Owner.SetDefaultGoal(VAgentGoal.Cannibalize);
						Owner.SetCannibalizingTarget(corpse);
						Owner.losCheckAtIntervalsTime = -25;
						Owner.noEnforcerAlert = true;
						break;
					}
				}
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Eat_Corpses>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will eat corpses like the Cannibal."),
					[LanguageCode.Spanish] = string.Format("Este NPC se pasara a los cadaveres que vea por los dientes!"),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Eat_Corpses)),
					[LanguageCode.Spanish] = "Come-Cadaveres",

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
