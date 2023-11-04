﻿using BepInEx.Logging;
using RogueLibsCore;
using System;

namespace CCU
{
	public abstract class T_DesignerTrait : CustomTrait
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static TraitBuilder PostProcess_DesignerTrait
		{
			set
			{
				value.Unlock.CharacterCreationCost = 0;
				value.Unlock.IsAvailable = false;
				value.Unlock.IsAvailableInCC = Core.designerEdition;
				value.Unlock.UnlockCost = 0;

				value.Unlock.Unlock.cantLose = true;
				value.Unlock.Unlock.cantSwap = true;
				value.Unlock.Unlock.upgrade = null;
			}
		}

		public static string DesignerName(Type type, string custom = null) =>
			$"{Core.CCUBlockTag} {GroupFromNamespace(type)} - {custom ?? Prettify(type.Name)}";

		public static string DocumentationName(Type type) =>
			$"{GroupFromNamespace(type)} - {Prettify(type.Name)}";

		// TODO: Eliminate this. Make an abstract group name string field to be filled out per group.
		public static string GroupFromNamespace(Type type) =>
			Prettify(type.Namespace.Split('.')[2]);

		public static string Prettify(string original) =>
			original
				.Replace('_', ' ')
				.Trim();

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}