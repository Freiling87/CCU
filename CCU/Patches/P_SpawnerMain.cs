using BepInEx.Logging;
using CCU.Traits.Drug_Warrior_Modifier;
using HarmonyLib;
using RogueLibsCore;
using UnityEngine.Networking;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(SpawnerMain))]
    public static class P_SpawnerMain
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.SpawnStatusText), argumentTypes: new[] 
            { typeof(PlayfieldObject), typeof(string), typeof(string), typeof(string), typeof(NetworkInstanceId), typeof(string), typeof(string) })]
        public static bool SpawnStatusText_Prefix(PlayfieldObject myPlayfieldObject, string myText)
        {
            if (myPlayfieldObject.CompareTag("Agent") && myText == vItem.Syringe)
            {
                Agent agent = myPlayfieldObject.GetComponent<Agent>();

                if (agent.HasTrait<Suppress_Syringe_AV>())
                    return false;
            }

            return true;
        }
    }
}