using BepInEx;
using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;

namespace CCU.Traits
{
    public class TraitUnlock_CCU : TraitUnlock
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public TraitUnlock_CCU() : base() { }
        public TraitUnlock_CCU(string key, string traitGroup) : base(key)
        {
            Key = key;
            TraitNameSubstring = traitGroup;
        }

        public bool IsPlayerTrait = false;
        readonly string Key = "";
        readonly string TraitNameSubstring = "";
        //bool expanded = false;

        //TODO: Move this to its own subtype, since it currently applies to all traits.
        //     public override void OnPushedButton()
        //     {
        //         BaseUnityPlugin rl = RogueFramework.Plugin;
        //         Type rlType = rl.GetType();
        //         CharacterCreation CC = GC.mainGUI.characterCreationScript;
        //         float state = 0f;

        //         logger.LogDebug("Prefix");
        //         MethodInfo prefix = rlType.GetMethod("ScrollingMenu_OpenScrollingMenu_Prefix");
        //         object[] parameters = new object[] { GC.mainGUI.scrollingMenuScript, state };
        //         prefix.Invoke(null, parameters);

        //         #region Original, Verified
        //         logger.LogDebug("Original");
        //         PlaySound("ClickButton");
        //         expanded = !expanded;

        //         foreach (DisplayedUnlock displayedUnlock in Menu.Unlocks)
        //         {
        //             if (displayedUnlock.GetFancyName().Contains("[CCU] " + TraitNameSubstring))
        //             {
        //                 IUnlockInCC unlockInCC = displayedUnlock as IUnlockInCC;
        //                 unlockInCC.IsAvailableInCC = expanded;
        //             }
        //         }
        //         #endregion
        //         #region OpenCharacterCreation
        //         logger.LogDebug("Copied portion from OpenCharacterCreation");

        //         for (int i = 0; i < CC.numButtonsTraits; i++)
        //         {
        //             ButtonData buttonData = new ButtonData();
        //             buttonData.mainGUI = CC.agent.mainGUI;
        //             // buttonData.isActive = true;
        //             buttonData.worldSpaceGUI = CC.agent.worldSpaceGUI;
        //             buttonData.characterCreation = CC;
        //             buttonData.resizeTextForBestFit = true;
        //             buttonData.scrollingButtonNum = i;
        //             buttonData.agent = CC.agent;
        //             buttonData.scrollingButtonUnlock = null; 
        //             buttonData.scrollingButtonItem = null;
        //             buttonData.scrollingButtonTrait = null;
        //             buttonData.scrollingButtonAgentName = "";
        //             buttonData.scrollingButtonLevelFeeling = "";
        //             buttonData.canvasGroupAlpha = 1;
        //             buttonData.menuType = "Traits";
        //             CC.buttonsDataTraits.Add(buttonData);
        //             CC.SetupTraits(buttonData, CC.listUnlocksTraits[i]);
        //         }

        //foreach (ButtonData buttonData in CC.buttonsDataTraits)
        //	buttonData.canvasGroupAlpha = 1;

        //CC.refreshing = true;
        //CC.traitsMenu.gameObject.SetActive(true);
        //CC.scrollerControllerTraits.RefreshMenuData(false);
        //         #endregion

        //         logger.LogDebug("Postfix");
        //         MethodInfo postfix = rlType.GetMethod("ScrollingMenu_OpenScrollingMenu");
        //         parameters = new object[] { GC.mainGUI.scrollingMenuScript, state, CC.listUnlocksTraits };
        //         postfix.Invoke(null, parameters);

        //         // Didn't work: 

        //         // ScrollingMenu scrollingMenu = gc.mainGUI.scrollingMenuScript;
        //         // foreach (ButtonData buttonData in scrollingMenu.buttonsData)
        //         //     scrollingMenu.SetupTraitUnlocks(buttonData, buttonData.scrollingButtonUnlock);
        //         // scrollingMenu.UpdateOtherVisibleMenus(scrollingMenu.menuType);


        //         // Menu.UpdateMenu();


        //         // GC.mainGUI.characterCreationScript.scrollerControllerTraits.myScroller.RefreshActiveCellViews();


        //         // gc.mainGUI.characterCreationScript.scrollerControllerTraits.RefreshMenuData(false);


        //         // gc.mainGUI.scrollingMenuScript.OpenScrollingMenu();


        //         // gc.mainGUI.characterCreationScript.OpenCharacterCreation(false);


        //         // gc.mainGUI.characterCreationScript.OpenCharacterCreation(true);


        //     }
    }
}