using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits
{
    public abstract class T_CCU : CustomTrait
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public static TraitBuilder PostProcess
        {
            // This is applied after each trait is created. I don't fully get how it works, but see where PP's value is assigned.
            set
            {
                value.Unlock.Unlock.cantLose = true;
                value.Unlock.Unlock.cantSwap = true;
                value.Unlock.Unlock.upgrade = null;
            }
        }

        public static List<Unlock> AlphabetizeUnlockList(List<Unlock> original) =>
            original.OrderBy(u => GC.nameDB.GetName(u.unlockName, "StatusEffect")).ToList();

        public static List<Trait> DesignerTraitList(List<Trait> original) =>
            original.Where(t => IsDesignerTrait(t)).ToList();
        public static List<Unlock> DesignerUnlockList(List<Unlock> original) =>
            original.Where(u => IsDesignerUnlock(u)).ToList();

        public static List<Trait> PlayerTraitList(List<Trait> original) =>
            original.Where(t => IsPlayerTrait(t)).ToList();
        public static List<Unlock> PlayerUnlockList(List<Unlock> original) =>
            original.Where(u => IsPlayerUnlock(u)).ToList();

        public static List<Unlock> SortUnlockListByCCP(List<Unlock> original) =>
            original.OrderBy(u => u.cost3).ToList();

        public static List<Unlock> VanillaTraitList(List<Unlock> original) =>
            original.Where(u => !(u.GetHook() is TraitUnlock_CCU)).ToList();

        public static bool IsDesignerTrait(Trait trait) =>
            !(trait?.GetHook<T_CCU>() is null) &&
            trait?.GetHook<T_PlayerTrait>() is null;
        public static bool IsDesignerUnlock(Unlock unlock) =>
            unlock.GetHook() is TraitUnlock_CCU traitUnlock_CCU && !traitUnlock_CCU.PlayerTrait;

        public static bool IsPlayerTrait(Trait trait) =>
            trait?.GetHook<T_CCU>() is null ||
            !(trait?.GetHook<T_PlayerTrait>() is null);
        public static bool IsPlayerUnlock(Unlock unlock) =>
            !(unlock.GetHook() is TraitUnlock_CCU) ||
            (unlock.GetHook() is TraitUnlock_CCU traitUnlock_CCU && traitUnlock_CCU.PlayerTrait);

        public string TextName => 
            DesignerName(GetType());

        public static string DesignerName(Type type, string custom = null) =>
            "[CCU] " + 
            (type.Namespace).Split('.')[2].Replace('_', ' ') + 
            " - " +
            (custom ?? (type.Name).Replace('_', ' '));

        public static string LongishDocumentationName(Type type) =>
            (type.Namespace).Split('.')[2].Replace('_', ' ') +
            " - " +
            (type.Name).Replace('_', ' ');

        public static string PlayerName(Type type) =>
            (type.Name).Replace('_', ' ');
    }
}