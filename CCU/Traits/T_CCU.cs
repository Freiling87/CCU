using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU
{
	public abstract class T_CCU : CustomTrait
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	IRefreshPerLevel
		public bool AlwaysApply => false;
		public static TraitBuilder PostProcess
		{
			// This is applied after each trait is created. I don't fully get how it works, but see where PP's value is assigned.
			set
			{
				// Disabled this 2023-10-01... why was it made? It seems to be for Designer traits.
				//value.Unlock.Unlock.cantLose = true;
				//value.Unlock.Unlock.cantSwap = true;
				//value.Unlock.Unlock.upgrade = null;
			}
		}

		public static List<Trait> DesignerTraitList(List<Trait> original) =>
			original.Where(t => IsDesignerTrait(t)).ToList();
		public static List<Unlock> DesignerUnlockList(List<Unlock> original) =>
			original.Where(u => IsDesignerUnlock(u)).ToList();

		public static List<Trait> PlayerTraitList(List<Trait> original) =>
			original.Where(t => IsPlayerTrait(t)).ToList();
		public static List<Unlock> PlayerUnlockList(List<Unlock> original) =>
			original.Where(u => IsPlayerUnlock(u)).ToList();
		 
		public static List<Unlock> SortUnlocksByValue(List<Unlock> original) =>
			original.OrderBy(u => u.cost3).ToList();
		public static List<Unlock> SortUnlocksByName(List<Unlock> original) =>
			original.OrderBy(u => GC.nameDB.GetName(u.unlockName, "StatusEffect")).ToList();

		public static List<Unlock> VanillaTraitList(List<Unlock> original) =>
			original.Where(u => !(u.GetHook() is TraitUnlock_CCU)).ToList();

		public static bool IsDesignerTrait(Trait trait) =>
			!(trait?.GetHook<T_CCU>() is null) &&
			trait?.GetHook<T_PlayerTrait>() is null;
		public static bool IsDesignerUnlock(Unlock unlock) =>
		   unlock.GetHook() is TraitUnlock_CCU traitUnlock_CCU &&
		   !traitUnlock_CCU.IsPlayerTrait;

		public static bool IsPlayerTrait(Trait trait) =>
			trait?.GetHook<T_CCU>() is null ||
			!(trait?.GetHook<T_PlayerTrait>() is null);
		public static bool IsPlayerUnlock(Unlock unlock) =>
			!(unlock.GetHook() is TraitUnlock_CCU) ||
			(unlock.GetHook() is TraitUnlock_CCU traitUnlock_CCU && traitUnlock_CCU.IsPlayerTrait);

		// Get rid of this
		public string TextName =>
			DesignerName(GetType());

		// Major issue: This is hardcoded with namespaces, and namespaces have recently been fucked with.
		public static string DesignerName(Type type, string custom = null) =>
			"[CCU] " +
			(type.Namespace).Split('.')[2].Replace('_', ' ') +
			" - " +
			(custom ?? (type.Name).Replace('_', ' '));

		// Major issue: This is hardcoded with namespaces, and namespaces have recently been fucked with.
		public static string LongishDocumentationName(Type type) =>
			(type.Namespace).Split('.')[2].Replace('_', ' ') +
			" - " +
			(type.Name).Replace('_', ' ');

		public static string PlayerName(Type type) =>
			(type.Name)
				.Replace('_', ' ')
				.Replace("2", "+");

		// This makes override in child classes optional.
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}