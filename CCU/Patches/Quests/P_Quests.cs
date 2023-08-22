using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Patches.AgentQuests
{
	[HarmonyPatch(declaringType: typeof(Quests))]
    public static class P_Quests
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(Quests.CheckIfBigQuestObject))]
		private static IEnumerable<CodeInstruction> MakeScumbag(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo scumbagSoftcode = AccessTools.DeclaredMethod(typeof(P_Quests), nameof(P_Quests.ScumbagSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.GangsterCrepe),
					new CodeInstruction(OpCodes.Call),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Brtrue),
                },
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, scumbagSoftcode)
				});
			 
			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool ScumbagSoftcode(PlayfieldObject playfieldObject) =>
			playfieldObject.CompareTag("Agent")
				? ((Agent)playfieldObject).HasTrait<Scumbag>() || playfieldObject.objectName == VanillaAgents.GangsterCrepe
				: false;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Quests.setupQuests))]
		private static IEnumerable<CodeInstruction> ScreenContainersForBombDisaster(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo chestDic = AccessTools.DeclaredField(typeof(GameController), nameof(GameController.chestDic));
			MethodInfo filteredChestDic = AccessTools.DeclaredMethod(typeof(P_Quests), nameof(P_Quests.FilteredChestDic));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 3,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, chestDic),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filteredChestDic),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static Dictionary<int, ObjectReal> FilteredChestDic(Dictionary<int, ObjectReal> original)
		{
			Dictionary<int, ObjectReal> final = 
				original
				.Where(pair => IsValidBombContainer(pair.Value))
				.ToDictionary(pair => pair.Key, pair => pair.Value);

			return final;
		}

		private static bool IsValidBombContainer(ObjectReal bombContainer)
		{
			if (bombContainer is Tube tube)
			{
				foreach (ObjectReal powerObject in GC.objectRealList)
					if ((powerObject is Computer computer
							&& (computer.startingChunk == tube.startingChunk
								|| (computer.startingSector == tube.startingSector && computer.startingSector != 0)))
						|| (powerObject is PowerBox powerBox
							&& AffectedChunks(powerBox).Contains(tube.startingChunk)))
						return true;

				return false;
			}
			
			return true;
		}
		// Adapted from PowerBox.ShutDown & PowerBox.AddAffectedChunk
		private static List<int> AffectedChunks(PowerBox powerBox)
		{
			float num = 10.24f;
			List<int> chunks = new List<int>();
			List<Vector2> ChunkNeighbors = new List<Vector2>()
			{
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y - num),
				new Vector2(powerBox.tr.position.x + num, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y - num),
				new Vector2(powerBox.tr.position.x - num, powerBox.tr.position.y),
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y + num),
				new Vector2(powerBox.tr.position.x, powerBox.tr.position.y - num)
			};

			foreach (Vector2 vector2 in ChunkNeighbors)
			{
				int chunkID = powerBox.gc.tileInfo.GetTileData(new Vector2(vector2.x, vector2.y)).chunkID;

				if (!chunks.Contains(chunkID) && chunkID != 0)
					chunks.Add(chunkID);
			}

			return chunks;
		}
	}
}