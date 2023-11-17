using BepInEx.Logging;
using BunnyLibs;
using CCU.Systems.Appearance;
using CCU.Traits.App_AC1;
using CCU.Traits.App_AC3;
using CCU.Traits.App_BC1;
using CCU.Traits.App_BC3;
using CCU.Traits.App_BT1;
using CCU.Traits.App_EC1;
using CCU.Traits.App_EC3;
using CCU.Traits.App_ET1;
using CCU.Traits.App_ET3;
using CCU.Traits.App_FH1;
using CCU.Traits.App_FH3;
using CCU.Traits.App_HC1;
using CCU.Traits.App_HC3;
using CCU.Traits.App_HS1;
using CCU.Traits.App_HS2;
using CCU.Traits.App_HS3;
using CCU.Traits.App_LC1;
using CCU.Traits.App_LC3;
using CCU.Traits.App_SC1;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

/// WARNING
/// Yes, this is awful code! I hate looking at it, I hate thinking about it.
/// I've attempted refactor about three times, wasting several hours before realizing it's not worth trying to get a perfect system here, because the base game's code is what it is and I'm forced to work with it. 
/// Any suggestions are appreciated, but it's gonna stay ugly for now.

namespace CCU.Traits.App
{
	public abstract class T_Appearance : T_DesignerTrait
	{
		public T_Appearance() : base() { }

		public abstract string[] Rolls { get; }
	}

	[HarmonyPatch(typeof(SpawnerMain))]
	public class P_SpawnerMain_Appearance
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch("ActionsAfterAgentSpawn")]
		public static void RollAppearanceFriendPhone(Agent myAgent, string spawnType)
		{
			if (spawnType == VItemName.FriendPhone)
			{
				AppearanceTools.SetupAppearance(myAgent.agentHitboxScript);
			}
		}
	}
}