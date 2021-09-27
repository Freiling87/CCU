using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using System.Reflection;
using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;
using CCU.Traits;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(AgentHitbox))]
	public static class P_AgentHitbox
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// TODO: This is where facial hair is drawn randomly. Set it to happen if they have appearance traits.
		// Do a replacement patch for now.
		// Do a transpiler later when those tools are easier to use.

		// See chat logs for specifics of what to mod - I believe you just need to add a Linq statement to a single List.
		// Target: AgentHitbox.SetupFeatures

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> SetupFeatures_Transpiler(IEnumerable<CodeInstruction> instructionsEnumerable, ILGenerator generator)
		{
			List<CodeInstruction> instructions = instructionsEnumerable.ToList();
			SetupFeatures_Hook(generator).ApplySafe(instructions, logger);
			return instructions;
		}

		public static CodeReplacementPatch SetupFeatures_Hook(ILGenerator generator) =>
			GetInteractionPatch(generator, nameof(RollCustomAppearance));

		private static void RollCustomAppearance(AgentHitbox agentHitBox)
		{
			logger.LogInfo("RollCustomAppearance");

			if (agentHitBox.agent.agentName == "Custom" && agentHitBox.agent.isPlayer == 0)
			{
				Appearance.RollFacialHair(agentHitBox, agentHitBox.agent);
				Appearance.RollHairstyle(agentHitBox, agentHitBox.agent);
				Appearance.RollSkinColor(agentHitBox, agentHitBox.agent);

				agentHitBox.SetCantShowHairUnderHeadPiece();

				Appearance.RollHairColor(agentHitBox, agentHitBox.agent);
			}
		}

		private static CodeReplacementPatch GetInteractionPatch(ILGenerator generator, string handler)
		{
			Label continueLabel = generator.DefineLabel();

			MethodInfo handlerMethod = AccessTools.Method(typeof(P_AgentHitbox), handler, new Type[1] { typeof(AgentHitbox) });

			return new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					// IL_05C7: stelem    [UnityEngine.CoreModule]UnityEngine.Color32
					new CodeInstruction(OpCodes.Stelem),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// RollCustomAppearance(agentHitbox)
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, handlerMethod)

				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// IL_05CC: nop
					new CodeInstruction(OpCodes.Nop),
				}
			);
		}
	}
}
