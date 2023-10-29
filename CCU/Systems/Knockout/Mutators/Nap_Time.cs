using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Knockout
{
	public class Nap_Time : M_CCU
	{
		public Nap_Time() : base(nameof(Nap_Time), true) { }

		//[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Nap_Time())
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Certain damage types temporarily knock people out instead of killing them. They may wake up with an attitude adjustment.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Nap_Time)),
				});
		}
	}

	[HarmonyPatch(typeof(StatusEffects))]
	internal class P_StatusEffects_NapTime
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Transpiler into StatusEffects.StatusEffectsUpdate @ "if (this.agent.isplayer > 0)"
		//	To enable wakeup from knockout
	}
}