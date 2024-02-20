using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Heal_Player : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.Heal;
		public override string MoneyCostName => VDetermineMoneyCost.Heal;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Heal_Player>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can heal, for money.\n\nThey may or may not keep it real."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Heal_Player)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.Say), new[] { typeof(string), typeof(bool) })]
		public static bool Say_Prefix(ref string myMessage)
		{
			if (myMessage == "E_CantHeal")
				myMessage = "Doctor_CantHeal";

			return true;
		}
	}
}