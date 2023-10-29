using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;
using UnityEngine.Networking;

namespace CCU.Traits.Passive
{
	// Named because adding a trait with the same name as a status effect will just give you the status effect. Code you can smell before you even see it!
	public class Not_Vincible : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Not_Vincible>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Go ahead, try to vince them. They simply can't be vinced."),
					[LanguageCode.Spanish] = "Este NPC no puede ser vencido por un ser tan debil. Nota: jugar con este personaje puede causar bugs extraños no esperados.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Not_Vincible)),
					[LanguageCode.Spanish] = "No Vencible",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;


		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.ChangeHealth),
			new[] { typeof(float), typeof(PlayfieldObject), typeof(NetworkInstanceId), typeof(float), typeof(string), typeof(byte) })]
		public static bool ChangeHealth_Prefix(StatusEffects __instance, ref float healthNum)
		{
			if (__instance.agent.HasTrait<Not_Vincible>() && healthNum < 0f)
				healthNum = 0f;

			return true;
		}
	}
}