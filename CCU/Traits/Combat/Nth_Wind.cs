using BepInEx.Logging;
using CCU.Patches.Agents;
using CCU.Traits.Drug_Warrior;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Combat
{
    public class Nth_Wind : T_Combat
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Nth_Wind>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Refreshes certain flags after combat ends, e.g. Drug Warrior, allowing them to be used across multiple combats."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Nth_Wind)),
                    
                })
                .WithUnlock(new TraitUnlock
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

        public static void ResetFlags(Agent agent)
        {
            if (agent.HasTrait<Nth_Wind>())
            {
                if (agent.GetTraits<T_DrugWarrior>().Any())
                {
                    agent.combat.canTakeDrugs = true;
                    agent.oma.combatTookDrugs = false;
                }

                if (agent.HasTrait<Backed_Up>())
                    agent.GetHook<P_Agent_Hook>().WalkieTalkieUsed = false;
            }
        }
    }
}
