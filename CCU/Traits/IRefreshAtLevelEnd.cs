using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits
{
	public interface IRefreshAtLevelEnd
    {
        void RefreshAtLevelEnd(Agent agent);
    }

    // Import: P_ExitPoint
    // Subclasses: Homesickly, Homesickless
}