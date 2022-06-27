using CCU.Patches.Agents;
using CCU.Traits.Drug_Warrior;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Combat
{
    public class Nth_Wind : T_Combat
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Nth_Wind>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Refreshes certain flags after combat ends, e.g. Drug Warrior and Backed Up, allowing them to be used across multiple combats."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Nth_Wind)),
                    
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
                    agent.combat.canTakeDrugs = true;

                if (agent.HasTrait<Backed_Up>())
                    agent.GetHook<P_Agent_Hook>().HasUsedWalkieTalkie = false;
            }
        }
    }
}
