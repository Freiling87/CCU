using BTHarmonyUtils.InstructionSearch;
using BTHarmonyUtils.MidFixPatch;
using HarmonyLib;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Systems.Vanilla_Fixes
{
	[HarmonyPatch(typeof(Agent))]
	internal class Combat_Skills_Scaled
	{
		[BTHarmonyMidFix(nameof(SetupAgentStats_InitCustomCharacter_Matcher))]
		[HarmonyPatch(nameof(Agent.SetupAgentStats), new[] { typeof(string) })]
		private static void SetupAgentStats_InitCustomCharacter_MidFix(Agent __instance)
		{
			// melee- and gunSkill control:
			// - how frequently the agent performs actions,
			// - how likely he is to attack,
			// - how aggressively he performs in combat
			__instance.modGunSkill = Math.Min(2, __instance.customCharacterData.accuracy); // max supported gunSkill is 2
			__instance.modMeleeSkill = Math.Min(2, __instance.customCharacterData.strength); // max supported meleeSkill is 2
		}

		private static MidFixInstructionMatcher SetupAgentStats_InitCustomCharacter_Matcher()
		{
			FieldInfo field_customCharacterData = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.customCharacterData));
			FieldInfo field_strength = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.strength));

			return new MidFixInstructionMatcher(
					expectedMatches: 1,
					postfixInstructionSequence: new[] {
							InstructionMask.MatchOpCode(OpCodes.Ldarg_0),
							InstructionMask.MatchOpCode(OpCodes.Ldarg_0),
							InstructionMask.MatchInstruction(OpCodes.Ldfld, field_customCharacterData),
							InstructionMask.MatchInstruction(OpCodes.Ldfld, field_strength),
							InstructionMask.MatchOpCode(OpCodes.Call),
					}
			);
		}
	}
}