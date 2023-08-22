using BepInEx.Logging;
using CCU.Mutators;
using CCU.Systems.Tools;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace CCU.Systems.Mutator_Configurator
{
	internal static class MutatorConfigurator
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		// NOTE: ButtonData.ScrollingButtonType is the key name for buttons.
		// NOTE: Since sequential scrolling menus are not native, you might need to see what happens when they get  closed and make sure you're not leaving something in the gaps.

		static LevelEditor Level_Editor => GC.levelEditor;

		// Make as many of these private as possible.
		internal static bool IsActive;
		internal static List<M_MenuHead> MenuHeadSingletons = new List<M_MenuHead>();
		internal static M_MenuHead CurrentHead;
		internal static SubMenu CurrentSubmenu;
		internal static int SubmenuIndex =>
			CurrentHead?.SubMenus?.IndexOf(CurrentSubmenu) ?? 0;
		private static List<string> CompletedElements;

		[RLSetup]
		private static void Setup()
		{
			string t = "Interface";
			RogueLibs.CreateCustomName(ButtonTextBack, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Back",
			});
			RogueLibs.CreateCustomName(ButtonTextNext, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Next",
			});
		}

		internal static void StartConfigurator(M_MenuHead menuHead)			//	Based on LevelEditor.PressedLoadMutatorsLevel
		{	
			CurrentHead = menuHead;
			CompletedElements = new List<string>() { CurrentHead.HeaderElement };
			IsActive = true;
			GC.audioHandler.Play(GC.playerAgent, VanillaAudio.QuestAccept);

			// TODO: Detect existing configured mutators. Offer to overwrite them or back out.

			Level_Editor.scrollingMenuType = CurrentHead.DataHeader;
			ShowSubmenu(CurrentHead.SubMenus[0]);
		}
		internal static void PressedBack()
		{
			if (SubmenuIndex == 0)
			{
				CloseConfigurator();
				return;
			}

			CompletedElements.RemoveAt(CompletedElements.Count - 1);
			ShowSubmenu(CurrentHead.SubMenus[SubmenuIndex - 1]);
		}
		internal static void PressedNext()
		{
			if (!CurrentSubmenu.ValidateCurrentSelections(out _))
			{
				GC.audioHandler.Play(GC.playerAgent, VanillaAudio.CantDo);
				return;
			}

			if (CurrentSubmenu.CurrentSelections.Count > 0)
				CompletedElements.Add(CurrentSubmenu.FinalizedElementText());

			if (SubmenuIndex < CurrentHead.SubMenus.Count - 1)
				ShowSubmenu(CurrentHead.SubMenus[SubmenuIndex + 1]);
			else
				Complete();
		}
		internal static void Complete()
		{
			List<List<string>> userConfiguration = CurrentHead.SubMenus.Select(sm => sm.CurrentSelections).ToList();
			int label = 1;
			string displayName = null;

			while (RogueFramework.Unlocks.Any(uw => uw.Unlock.unlockName == displayName) || displayName is null)
				displayName = $"[CCU] {CurrentHead.DisplayHeader} Mutator (<color=yellow>{label++:D3}</color>)";

			string dataName = StringMonster.GetDataName(CompletedElements);
			M_CCU mutator = CurrentHead.CreateMutatorInstance(dataName, true);
			string description = GetFinalMutatorDescription();
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(mutator)
				.WithDescription(new CustomNameInfo 
				{
					[LanguageCode.English] = description,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = displayName,
				});
	
			GC.audioHandler.Play(GC.playerAgent, VanillaAudio.QuestCompleteBig);
			CloseConfigurator();
		}
		internal static void CloseConfigurator()							//	Based on LevelEditor.PressedCancelLoad
		{
			foreach (SubMenu subMenu in CurrentHead.SubMenus)
				subMenu.Reset();

			CurrentHead = null;
			CurrentSubmenu = null;
			CompletedElements.Clear();
			IsActive = false;
			Level_Editor.DeactivateLoadMenu();
			Level_Editor.PressedLoadMutatorsLevel(); // TODO: Branch this according to which menu started the configurator.
		}
		internal static void OnButtonPressed(ButtonHelper buttonHelper)
		{
			switch (buttonHelper.scrollingButtonType)
			{
				case "ClearAll":
					foreach (ButtonData buttonData2 in buttonHelper.scrollingMenu.buttonsData)
					{
						if (buttonData2.scrollingHighlighted)
						{
							buttonData2.scrollingHighlighted = false;
							buttonData2.highlightedSprite = buttonHelper.solidObjectButton;
						}
					}

					break;

				case "SelectAll":

					break;
			}

			CurrentSubmenu.OnButtonPressed(buttonHelper);
		}
		internal static void OpenObjectLoadCustom(List<string> menuEntries) //	Based on LevelEditor.OpenObjectLoad
		{
			Level_Editor.scrollBarLoad.gameObject.SetActive(menuEntries.Count >= 16);

			foreach (ButtonData buttonData in Level_Editor.buttonsDataLoad)
			{
				if (buttonData != null)
					buttonData.isActive = false;

				buttonData.scrollingHighlighted = false;
				buttonData.scrollingHighlighted2 = false;
				buttonData.scrollingHighlighted3 = false;
				buttonData.highlightedSprite = GC.mainGUI.characterCreationScript.solidObjectButton;
				buttonData.interactable = true;
			}
			Level_Editor.buttonsDataLoad.Clear();

			float y = menuEntries.Count * 46f; 
			RectTransform component = Level_Editor.contentLoad.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.rect.width, y);
			Level_Editor.scrollBarLoad.value = 1f;
			Level_Editor.StartCoroutine(Level_Editor.MakeButtonsVisible2());

			for (int i = 0; i < menuEntries.Count; i++)
			{
				ButtonData buttonData2 = new ButtonData
				{
					scrollingButtonType = menuEntries[i],
					agent = GC.playerAgent,
					buttonText = menuEntries[i],
					highlightedSprite = GC.mainGUI.characterCreationScript.solidObjectButton,
					interactable = true,
					isActive = true,
					resizeTextForBestFit = true,
					scrollingHighlighted = false,
					scrollingHighlighted2 = false,
					scrollingHighlighted3 = false,
				};

				Level_Editor.buttonsDataLoad.Add(buttonData2);
			}

			int j = 0;
			foreach (ButtonData buttonData in Level_Editor.buttonsDataLoad)
				buttonData.scrollingButtonNum = j++;

			Level_Editor.scrollerControllerLoad.RefreshMenuData(true);
			Level_Editor.scrollBarLoad.value = 1f;
		}
		internal static void ShowSubmenu(SubMenu subMenu)                   //	Based on LevelEditor.CreateMutatorListLevel
		{
			CurrentSubmenu = subMenu;
			UpdateInterface();
			FieldInfo numButtonsLoad = AccessTools.DeclaredField(typeof(LevelEditor), "numButtonsLoad");
			numButtonsLoad.SetValue(GC.levelEditor, (float)subMenu.Entries.Count);
			OpenObjectLoadCustom(subMenu.Entries);
			Level_Editor.StartCoroutine("SetScrollBarPlacement");

			foreach (ButtonData button in Level_Editor.buttonsDataLoad.Where(b => CurrentSubmenu.CurrentSelections.Contains(b.scrollingButtonType)))
			{
				button.scrollingHighlighted = true;
				button.highlightedSprite = GC.mainGUI.scrollingMenuScript.solidObjectButtonSelected;
			}

			Level_Editor.scrollerControllerLoad.RefreshMenuData(true);
			Level_Editor.scrollerControllerLoad.myScroller.RefreshActiveCellViews();
		}
		internal static void UpdateInterface()								//	Based on LevelEditor.ActivateLoadMenu
		{
			// Top instructions
			Level_Editor.loadInstructions.text = CurrentSubmenu.Instructions;

			// Right window header
			Level_Editor.detailsTextTitleLoad.text = "<b>Custom Mutator Configurator</b>"; // TODO
			// Right window body text
			Level_Editor.detailsTextDescriptionLoad.text = GetConfiguratorTable();

			// Next button
			Level_Editor.loadMenuCancelButton.transform.Find("Text").GetComponent<Text>().text = GC.nameDB.GetName(ButtonTextNext, "Interface");
			Level_Editor.loadMenuCancelButton.SetActive(true);
			// Back button
			Level_Editor.loadMenuExtraButton.transform.Find("Text").GetComponent<Text>().text = GC.nameDB.GetName(ButtonTextBack, "Interface");
			Level_Editor.loadMenuExtraButton.SetActive(true);

			// Logo sprite
			Level_Editor.detailsTextImageLoad.sprite = Core.CCULogoSprite;
			Level_Editor.detailsTextImageLoad.enabled = true;

			Level_Editor.detailsTextImageColor.enabled = false;
			Level_Editor.chunkPreviewImage.SetActive(false);
			Level_Editor.HideTopMenus();
			Level_Editor.loadMenu.gameObject.SetActive(true);
		}
		internal static string GetConfiguratorTable()
		{
			string outputBuilder = CurrentHead.DisplayHeader + "\n";

			foreach (SubMenu subMenu in CurrentHead.SubMenus)
				outputBuilder += subMenu.ConfiguratorStatusTextCompleted();

			outputBuilder = StringTools.PadVertical(outputBuilder, 9);
			outputBuilder += $"{CurrentSubmenu.ConfiguratorStatusTextCurrent()}";
			outputBuilder = StringTools.PadVertical(outputBuilder, 12);
			outputBuilder += $"\t\t\t\t    Page {SubmenuIndex + 1} of {CurrentHead.SubMenus.Count}";
			outputBuilder = StringTools.PadVertical(outputBuilder, 20);
			string output = outputBuilder.ToString();
			return output;
		}
		internal static string GetFinalMutatorDescription()
		{
			string output = CurrentHead.DisplayHeader + "\n";

			foreach (SubMenu subMenu in CurrentHead.SubMenus)
				output += subMenu.ConfiguratorStatusTextCompleted();

			return output;
		}
		internal static void InitializeMutatorsFromSavedContent()
		{
			logger.LogDebug("=== Initializing mutator Unlocks from saved content");
			List<string> campaigns = GC.menuGUI.GetFullCampaignFileList();

			foreach (string campaign in campaigns)
			{
				CampaignData campaignData = GC.menuGUI.LoadFileCampaign(campaign.Replace("_xxD", ""), campaign.Contains("xxD"));

				foreach (LevelData level in campaignData.levelList)
				{
					foreach (string challenge in level.levelMutators)
					{
						if (challenge.Contains(Core.CCUBlockTag))
						{
							logger.LogDebug($"\t\t\tFound Custom Mutator: {challenge}");

							List<string> elements = StringMonster.GetElements(challenge);
							logger.LogDebug($"\t\t\t\tElements:           {elements.Count}");

							foreach(string element in elements)
							{
								logger.LogDebug($"\t\t\t\t\tElement:      {element}");
							}

							string dataName = StringMonster.GetDataName(elements);
							logger.LogDebug($"\t\t\t\tData Name:          {dataName}");

							string displayName = StringMonster.GetDisplayName(challenge);
							logger.LogDebug($"\t\t\t\tDisplay Name:       {displayName}");

							string dataHeader = StringMonster.GetDisplayHeader(challenge);
							logger.LogDebug($"\t\t\t\tData Header:        {dataHeader}");

							string className = "M_" + dataHeader;
							logger.LogDebug($"\t\t\t\tClass Name:         {className}");

							Type type = Type.GetType(className);
							logger.LogDebug($"\t\t\t\tType:               {type.Name}");

							if (type != null)
							{
								logger.LogDebug("Found Type!");
								// TODO: Store namespace + type statically in mutator classes, in order to properly create instance.
								M_CCU mutator = (M_CCU)Activator.CreateInstance(type);
								logger.LogDebug($"\t\t\t\tInstance: {mutator}");

								string description = "Placeholder Description"; 
								UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(mutator)
									.WithDescription(new CustomNameInfo
									{
										[LanguageCode.English] = description,
									})
									.WithName(new CustomNameInfo
									{
										[LanguageCode.English] = displayName,
									});
							}
						}
					}
				}
			}
		}

		internal const string 
			ButtonTextBack = "ConfiguratorBack",
			ButtonTextNext = "ConfiguratorNext",
			ConfiguratorBlock = "(<color=lime>Configurator</color>)",

			z = "";
	}

	[HarmonyPatch(typeof(GameController))]
	internal static class P_GameController_ConfiguratorSetup
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch("Awake")]
		private static void SetupConfigurator()
		{
			MutatorConfigurator.InitializeMutatorsFromSavedContent();
		}
	}

	[HarmonyPatch(typeof(LevelEditor))]
	internal static class P_LevelEditor_ConfiguratorInputs
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(LevelEditor.PressedScrollingMenuButton))]
		private static bool PressedScrollingMenuButton(ButtonHelper myButtonHelper)
		{
			string buttonType = myButtonHelper.scrollingButtonType;

			if (!MutatorConfigurator.IsActive)
			{
				if (!buttonType.Contains(Core.CCUBlockTag))
					return true;

				buttonType = buttonType.Replace(Core.CCUBlockTag + " ", "");
				M_MenuHead menuHead = MutatorConfigurator.MenuHeadSingletons.Where(mh => mh.IsInstance(buttonType)).FirstOrDefault();

				if (menuHead is null)
					return true; 

				MutatorConfigurator.StartConfigurator(menuHead);
			}
			else if (MutatorConfigurator.IsActive)
			{
				MutatorConfigurator.OnButtonPressed(myButtonHelper);
				MutatorConfigurator.UpdateInterface();
				return false;
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(LevelEditor.PressedCancelLoad))]
		private static bool PressedNextButton()
		{
			if (!MutatorConfigurator.IsActive)
				return true;
			
			MutatorConfigurator.PressedNext();
			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(LevelEditor.PressedExtraLoad))]
		private static bool PressedBackButton()
		{
			if (!MutatorConfigurator.IsActive)
				return true;

			MutatorConfigurator.PressedBack();
			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(LevelEditor.ShowDetails))]
		private static bool ShowDetails()
		{
			if (!MutatorConfigurator.IsActive)
				return true;

			MutatorConfigurator.UpdateInterface();
			return false;
		}
	}
}