using CCU.Traits.Behavior;
using RogueLibsCore;
using System;
using System.Linq;
using UnityEngine;

namespace CCU.Traits.LOS_Behavior
{
	public abstract class T_ItemPickup : T_LOSBehavior
	{
		/// <summary>
		/// See VItemCategory for vanilla values. 
		/// </summary>
		public virtual string[] GrabItemCategories { get; }

		/// <summary>
		/// For more fine-tuned choices than Categories. 
		/// </summary>
		public virtual string[] GrabItemNames { get; }

		//	ICheckAgentLOS
		public override int LOSInterval => 8;
		public override float LOSRange => 5f;

		public override void LOSAction()
		{
			if (!Owner.agentInvDatabase.hasEmptySlot())
				Owner.losCheckAtIntervalsTime = 50;
			else if (!Owner.hasEmployer) // Might want a way to bypass this
			{
				foreach (Item item in GC.itemList)
				{
					try
					{
						if ((item.invItem.Categories.Intersect(GrabItemCategories).Any() || GrabItemNames.Contains(item.itemName))
							&& (!item.objectSprite.dangerous || Owner.HasTrait<Accident_Prone>())
							&& !item.fellInHole
							&& item.curTileData.prison == Owner.curTileData.prison && (Owner.curTileData.prison <= 0 || Owner.curTileData.chunkID == item.curTileData.chunkID)
							&& !item.dontStealFromGround
							&& !GC.tileInfo.DifferentLockdownZones(Owner.curTileData, item.curTileData)
							&& Vector2.Distance(Owner.curPosition, item.curPosition) < LOSRange
							&& Owner.movement.HasLOSObjectNormal(item))
						{
							Owner.SetPreviousDefaultGoal(Owner.defaultGoal);
							Owner.SetDefaultGoal(VAgentGoal.GoGet);
							Owner.SetGoGettingTarget(item);
							Owner.stoleStuff = true;
							return;
						}
					}
					catch (Exception e)
					{
						//logger.LogError($"+++ Caught Exception for {item.itemName}:\n\t{e}");
					}

					//	Reset only if no valid items seen. This allows agent to pick up a group of items without delay in between, but without wasting cycles afterward.
					Owner.losCheckAtIntervalsTime = 0;
				}
			}
		}
	}
}