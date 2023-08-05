using BepInEx.Logging;
using CCU.Mutators.Interface;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.PLAYER
{
	public class Mapless : T_PlayerTrait
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        private static GameController GC => GameController.gameController;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Mapless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("No map available."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Mapless)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = -10,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    IsUnlocked = Core.debugMode,
                    UnlockCost = 20,
                    Unlock =
                    {
                        cantLose = false,
                        cantSwap = true,
                        cancellations =
						{
                            VanillaTraits.NoTeleports,
						},
                        categories = {  },
                    }
                });
        }
        public override void OnAdded()
        {
            ResetMinimapVars();
        }
        public override void OnRemoved()
        {
            ResetMinimapVars();
        }
        // Doesn't currently work
        public static bool IsMaplessActive()
		{
            bool result = 
                GC.playerAgentList.Any(a => a.HasTrait<Mapless>())
                || GC.challenges.Contains(nameof(No_Maps));

            // This might be before agents are created
            foreach(Agent agent in gc.playerAgentList)
			{
                logger.LogDebug("Agent: " + agent.agentRealName.PadRight(20) + " / trait: " + agent.HasTrait<Mapless>());
			}

            logger.LogDebug("IsMaplessActive: " + result);

            return result;
        }
            
        public static void ResetMinimapVars()
		{
            bool value = IsMaplessActive();

            if (!value)
                gc.sessionDataBig.minimapSetting = "AlwaysOn";
            else
                gc.sessionDataBig.minimapSetting = "Off";

            //gc.minimap.go.SetActive(value);
            //gc.minimap.canvas.enabled = value;
		}
        public static string MinimapSetting =>
            IsMaplessActive()
                ? "Off"
                : "AlwaysOn";
    }

    //[HarmonyPatch(typeof(MainGUI))]
    internal static class MainGUI_Mapless
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        private static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(nameof(MainGUI.RealStart))]
        private static bool RealStart()
        {
            logger.LogDebug("MainGUI.RealStart");
            GC.sessionDataBig.minimapSetting = Mapless.MinimapSetting;

            return true;
        }

        [HarmonyPrefix, HarmonyPatch(nameof(MainGUI.ShowMinimap))]
        private static bool GateShowMinimap()
		{
            logger.LogDebug("GateShowMinimap");

            if (Mapless.IsMaplessActive())
			{
                GC.mainGUI.HideMinimap(false);
                return false;
            }

            return true;
		}
    }

	//[HarmonyPatch(typeof(MenuGUI))]
    internal static class P_MenuGUI_Mapless
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        private static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(nameof(MenuGUI.SetMinimapText))]
        private static bool GateMap()
        {
            logger.LogDebug("MenuGUI.SetMinimapText");
            GC.sessionDataBig.minimapSetting = Mapless.MinimapSetting;

            return true;
		}
	}
}