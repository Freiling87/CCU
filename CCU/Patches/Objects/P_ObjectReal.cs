using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Patches.Inventory;
using CCU.Systems.Containers;
using CCU.Systems.Investigateables;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(ObjectReal))]
    public static class P_ObjectReal
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: "Start", argumentTypes: new Type[0] { })]
        public static bool Start_SetupInvDatabasesForContainers(ObjectReal __instance)
        {
            if (Containers.ContainerObjects.Contains(__instance.objectName))
            {
                if (__instance.GetComponent<InvDatabase>() is null)
                    __instance.objectInvDatabase = __instance.go.AddComponent<InvDatabase>();
            }

            return true;
        }
    }

    [HarmonyPatch(declaringType: typeof(ObjectReal))]
    static class P_ObjectReal_DestroyMe2
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyTargetMethod, UsedImplicitly]
        private static MethodInfo Find_MoveNext_MethodInfo() =>
            PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(ObjectReal), "DestroyMe2"));

        [HarmonyTranspiler, UsedImplicitly]
        private static IEnumerable<CodeInstruction> DestroyMe2_DontSpillNote(IEnumerable<CodeInstruction> codeInstructions)
        {
            List<CodeInstruction> instructions = codeInstructions.ToList();
            FieldInfo invItemList = AccessTools.DeclaredField(typeof(InvDatabase), nameof(InvDatabase.InvItemList));
            MethodInfo filteredList = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.FilteredInvItemList));

            CodeReplacementPatch patch = new CodeReplacementPatch(
                expectedMatches: 1,
                prefixInstructionSequence: new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldfld, invItemList), 
                },
                insertInstructionSequence: new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Call, filteredList)
                });

            patch.ApplySafe(instructions, logger);
            return instructions;
        }
    }
}