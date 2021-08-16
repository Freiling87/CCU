using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx;
using UnityEngine;
using RogueLibsCore;
using BepInEx.Logging;

namespace CCU
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    [BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
    public class Core : BaseUnityPlugin
    {
        public const string pluginGUID = "Freiling87.streetsofrogue.CCU";
        public const string pluginName = "Custom Content Utilities";
        public const string pluginVersion = "0.1.0";

        public static ManualLogSource MyLogger;

        public void Awake()
        {
            RogueLibs.LoadFromAssembly();
        }
    }

    public static class cTrait
	{
        public const string
            Faction_1_Aligned = "Faction 1 Aligned",
            Faction_1_Hostile = "Faction 1 Hostile",
            Faction_2_Aligned = "Faction 2 Aligned",
            Faction_2_Hostile = "Faction 2 Hostile",
            Faction_3_Aligned = "Faction 3 Aligned",
            Faction_3_Hostile = "Faction 3 Hostile",
            Faction_4_Aligned = "Faction 4 Aligned",
            Faction_4_Hostile = "Faction 4 Hostile";
    }
}
