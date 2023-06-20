﻿using BepInEx.Logging;
using CCU.Hooks;
using CCU.Localization;
using CCU.Traits;
using CCU.Traits.Player.Ammo;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CCU.Items
{
    [ItemCategories(RogueCategories.Usable, RogueCategories.Technology)]
    public class Debugulizer : I_CCU, IItemTargetable
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [RLSetup]
        public static void Setup()
        {
            ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<Debugulizer>()
                .WithName(new CustomNameInfo("Debugulizer"))
                .WithDescription(new CustomNameInfo("Does what I need it to do!"))
                .WithSprite(Properties.Resources.Debugulizer)
                .WithUnlock(new ItemUnlock
                {
                    CharacterCreationCost = 0,
                    IsAvailable = Core.debugMode,
                    LoadoutCost = 0,
                    UnlockCost = 0,
                });

            RogueLibs.CreateCustomAudio("Debugulizer_Use", Properties.Resources.ClassAware_Use, AudioType.WAV);
        }

        public override void SetupDetails()
        {
            Item.goesInToolbar = true;
            Item.hasCharges = true;
            Item.itemType = ItemTypes.Tool;
            Item.itemValue = 1;
            Item.initCount = 99;
            Item.rewardCount = 99;
            Item.stackable = true;
            Item.Categories = new List<string> { VItemCategory.Technology, VItemCategory.Usable };
        }

        public CustomTooltip TargetCursorText(PlayfieldObject target)
        {
            if (target is null || !(target is Agent))
                return "Choose Target Agent";
            else
                return "Valid target:";
        }

        public bool TargetFilter(PlayfieldObject target) =>
            target is Agent agent;

        public bool TargetObject(PlayfieldObject target)
        {
            if (!TargetFilter(target))
                return false;

            Agent agent = (Agent)target;

            Item.invInterface.HideTarget();
            Owner.gc.audioHandler.Play(Owner, "ClassAWare_Use");
            Owner.gc.spawnerMain.SpawnStateIndicator(agent, "HighVolume");

            agent.AddTrait<Ammo_Auteur>();

            return true;
        }
    }
}