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
        // so you can reference it in patches

        public void Awake()
        {
            MyLogger = Logger;
            Logger.LogMessage("Hello World!");
        }
    }
}
