using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Patches.Agents;
using CCU.Traits.App;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Appearance
{
    [HarmonyPatch(declaringType: typeof(AgentHitbox))]
	public static class P_AgentHitbox
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// This prevents custom appearance being wiped out.
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentHitbox.chooseFacialHairType))]
		private static bool ChooseFacialHairType_SkipCustoms(string agentName)
        {
			if (agentName == VanillaAgents.CustomCharacter)
				return false;

			return true;
        }

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetupBodyStrings))]
		private static IEnumerable<CodeInstruction> SetupBodyStrings(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.agent));
			FieldInfo customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo eyesType = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.eyesType));
			MethodInfo getAgentEyesType = AccessTools.DeclaredMethod(typeof(P_AgentHitbox), nameof(P_AgentHitbox.GetAgentEyesType));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, customCharacterData),
					new CodeInstruction(OpCodes.Ldfld, eyesType),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, getAgentEyesType)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures))]
		private static IEnumerable<CodeInstruction> SetupFeatures_Custom(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo hasSetup = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.hasSetup));
			MethodInfo setupAppearance = AccessTools.DeclaredMethod(typeof(AppearanceTools), nameof(AppearanceTools.SetupAppearance), parameters: new Type[] { typeof(AgentHitbox) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Stfld, hasSetup),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, setupAppearance),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Causes sprite updates to pull from the spawned character rather than the saved custom character.
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetWBSprites))]
		private static IEnumerable<CodeInstruction> SetWBSprites_FixBody_01(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.agent));
			MethodInfo convertIntToEyesType = AccessTools.DeclaredMethod(typeof(ObjectMultAgent), nameof(ObjectMultAgent.convertIntToEyesType));
			FieldInfo customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo bodyType = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.bodyType));
			MethodInfo eyesType2_get = AccessTools.PropertyGetter(typeof(ObjectMultAgent), nameof(ObjectMultAgent.eyesType));
			MethodInfo getAgentBodyType = AccessTools.DeclaredMethod(typeof(P_AgentHitbox), nameof(P_AgentHitbox.GetAgentBodyType));
			FieldInfo oma = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.oma));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 4,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Ldfld, customCharacterData),
					new CodeInstruction(OpCodes.Ldfld, bodyType),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),							//	this
					new CodeInstruction(OpCodes.Ldfld, agent),						//	this.agent
					new CodeInstruction(OpCodes.Call, getAgentBodyType),			//	string
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		// Causes sprite updates to pull from the spawned character rather than the saved custom character.
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetWBSprites))]
		private static IEnumerable<CodeInstruction> SetWBSprites_FixEyes_01(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.agent));
			MethodInfo convertIntToEyesType = AccessTools.DeclaredMethod(typeof(ObjectMultAgent), nameof(ObjectMultAgent.convertIntToEyesType));
			FieldInfo customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo eyesType = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.eyesType));
			MethodInfo eyesType2_get = AccessTools.PropertyGetter(typeof(ObjectMultAgent), nameof(ObjectMultAgent.eyesType));
			MethodInfo getAgentEyesType = AccessTools.DeclaredMethod(typeof(P_AgentHitbox), nameof(P_AgentHitbox.GetAgentEyesType));
			FieldInfo oma = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.oma));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Ldfld, customCharacterData),
					new CodeInstruction(OpCodes.Ldfld, eyesType),
					new CodeInstruction(OpCodes.Stloc_0),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),							//	this
					new CodeInstruction(OpCodes.Ldfld, agent),						//	this.agent
					new CodeInstruction(OpCodes.Call, getAgentEyesType),			//	string
					new CodeInstruction(OpCodes.Stloc_0),							//	
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		// Causes sprite updates to pull from the spawned character rather than the saved custom character.
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetWBSprites))]
		private static IEnumerable<CodeInstruction> SetWBSprites_FixEyes_02(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.agent));
			FieldInfo customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo eyesType = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.eyesType));
			MethodInfo getAgentEyesType = AccessTools.DeclaredMethod(typeof(P_AgentHitbox), nameof(P_AgentHitbox.GetAgentEyesType));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Ldfld, customCharacterData),
					new CodeInstruction(OpCodes.Ldfld, eyesType),
					new CodeInstruction(OpCodes.Stloc_3),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),							//	this
					new CodeInstruction(OpCodes.Ldfld, agent),						//	this.agent
					new CodeInstruction(OpCodes.Call, getAgentEyesType),			//	string
					new CodeInstruction(OpCodes.Stloc_3),							//	
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static string GetAgentBodyType(Agent agent) =>
			agent.GetOrAddHook<P_Agent_Hook>().bodyType;
		private static string GetAgentEyesType(Agent agent) =>
			agent.GetOrAddHook<P_Agent_Hook>().eyesType;
	}
}