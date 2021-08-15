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

            RoguePatcher patcher = new RoguePatcher(this);

            ChunkEditor.Awake();
            Factions.Awake();
        }

    }

    public static class cTrait
	{
        public const string
            Faction1 = "Faction1",
            Faction2 = "Faction2",
            Faction3 = "Faction3",
            Faction4 = "Faction4",
            Faction5 = "Faction5",
            Faction6 = "Faction6",
            Faction7 = "Faction7",
            Faction8 = "Faction8";
	}
}
