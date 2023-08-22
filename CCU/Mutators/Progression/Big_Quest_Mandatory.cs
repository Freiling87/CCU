using RogueLibsCore;
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

namespace CCU.Mutators.Progression
{
	class Big_Quest_Mandatory : M_Progression
	{
		public override bool RollInDailyRun => true;
		public override bool ShowInLevelMutatorList => true;

		//[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new M_CCU(nameof(Big_Quest_Mandatory), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If you fail your Big Quest, you fuckin' explode. YOU FUCKIN' EXPLODE!!!",
                    [LanguageCode.Spanish] = "Fallar tu Gran Misión causa el boiler que te robaste se prenda. Para gozar eres una BOMBA.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Big_Quest_Mandatory)),
                    [LanguageCode.Spanish] = "Gran Misión Obligatoria",
                });
		}
	}

	class P_Quests_BigQuestMandatory
	{

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Quests.SpawnBigQuestFailedText2))]
		private static void MandatoryBigQuest()
		{
			if (GC.challenges.Contains(nameof(Big_Quest_Mandatory)))
			{
				foreach (Agent player in GC.playerAgentList)
					player.StartCoroutine("SuicideWhenPossible");
			}
		}
	}
}