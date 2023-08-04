﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
	public class Intruders_Outlet : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Crowbar, 3),
            new KeyValuePair<string, int>( vItem.Lockpick, 3),
            new KeyValuePair<string, int>( vItem.SafeBuster, 3),
            new KeyValuePair<string, int>( vItem.CardboardBox, 3),
            new KeyValuePair<string, int>( vItem.WallBypasser, 3),
            new KeyValuePair<string, int>( vItem.WindowCutter, 3),
            new KeyValuePair<string, int>( vItem.BodySwapper, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Intruders_Outlet>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells intrusion tools. This is the vanilla Thief shop inventory\n\nYou're gonna like the shit you steal - I guarantee it!"),
                    [LanguageCode.Spanish] = "Este NPC vende herramientas de intrucion, todo lo que el ladron te vende en vanilla esta aqui.\n\nEstos precios son un robo!",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Intruders_Outlet), "Intruder's Outlet"),
                    [LanguageCode.Spanish] = "Entrada de Instrusos",

                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
