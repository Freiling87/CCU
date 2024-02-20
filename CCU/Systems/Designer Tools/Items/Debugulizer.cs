using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using UnityEngine;

namespace CCU.Items
{
	[ItemCategories(RogueCategories.Usable, RogueCategories.Technology)]
	public class Debugulizer : I_CCU, IItemTargetable
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		public static void Setup()
		{
			if (Core.developerEdition)
			{
				ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<Debugulizer>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Debugulizer",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Developer tool.",
				})
				.WithSprite(Properties.Resources.Debugulizer)
				.WithUnlock(new ItemUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.developerEdition,
					LoadoutCost = 0,
					UnlockCost = 0,
				});

				RogueLibs.CreateCustomAudio("Debugulizer_Use", Properties.Resources.ClassAware_Use, AudioType.WAV);
			}
		}

		public override void SetupDetails()
		{
			Item.goesInToolbar = true;
			Item.hasCharges = true;
			Item.itemType = ItemTypes.Tool;
			Item.itemValue = 1;
			Item.initCount = 99;
			Item.rewardCount = 99;
			Item.stackable = true;
			Item.Categories = new List<string> { VItemCategory.Technology, VItemCategory.Usable };
		}

		public CustomTooltip TargetCursorText(PlayfieldObject target)
		{
			if (target is null || !(target is Agent))
				return ClassAWare.targetingText;
			else
				return ClassAWare.validTargetText;
		}

		public bool TargetFilter(PlayfieldObject target) =>
			target is Agent;

		public bool TargetObject(PlayfieldObject target)
		{
			if (!TargetFilter(target))
				return false;

			Agent agent = (Agent)target;

			Item.invInterface.HideTarget();
			Owner.gc.audioHandler.Play(Owner, "ClassAWare_Use");
			Owner.gc.spawnerMain.SpawnStateIndicator(agent, "HighVolume");

			DebugActions(agent);

			return true;
		}

		public static void DebugActions(Agent agent)
		{
			agent.Suicide();
		}
	}
}