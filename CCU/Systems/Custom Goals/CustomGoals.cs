using BepInEx.Logging;
using CCU.Localization;
using CCU.Patches.Agents;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Systems.CustomGoals
{
    class CustomGoals
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public const string
            //  Scene Setters
            Arrested = "Arrested",
            Burned = "Burned",
            Dead = "Dead",
            Gibbed = "Gibbed",
            KnockedOut = "Knocked Out",
            Zombified = "Zombified",

            //  Other Customs
            Panic = "Panic",
            WanderAgents = "Wander Between Agents",
            WanderAgentsNonOwners = "Wander Between Agents (Non-Owner)",
            WanderAgentsOwners = "Wander Between Agents (Owner)",
            WanderObjects = "Wander Between Objects (Non-Owner)",
            WanderObjectsOwned = "Wander Between Objects (Owner)",

            //  Vanilla Unlocked
            CommitArson = "Commit Arson",
            FleeDanger = "Flee Danger",
            RobotClean = "RobotClean",

            NoMoreSemicolon = "";

        // Pseudo-Goals that trigger once on level load
        public static List<string> SceneSetters = new List<string>() 
        {
            Arrested,
            Burned,
            Dead,
            Gibbed,
            KnockedOut,
            Zombified,
        };
        public static List<string> ActualGoals = new List<string>()
        {
            //      Custom
            //  Investigate             //  Wander Agents + Wander Objects
            //  WanderAgents,           
            //  WanderAgentsNonOwners,  
            //  WanderAgentsOwners,
            //  WanderObjects,
            //  WanderObjectsOwned,

            //      Vanilla
            //CommitArson,
            //FleeDanger, // Probably not gonna work, and not really worth trying too hard on since it's unspecific.
            //RobotClean,
        };

        public static void RunSceneSetters()
        {
        start:
            foreach (Agent agent in GC.agentList)
            {
                if (!SceneSetters.Contains(agent.defaultGoal) || agent.GetHook<P_Agent_Hook>().SceneSetterFinished)
                    continue;

                switch (agent.defaultGoal)
                {
                    case Arrested:
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        // Copied from AgentInteractions.ArrestAgent
                        agent.knockedOut = true;
                        agent.knockedOutLocal = true;
                        agent.arrested = true;
                        agent.gc.tileInfo.DirtyWalls();
                        agent.gettingArrestedByAgent = agent;
                        agent.lastHitByAgent = agent.gettingArrestedByAgent;
                        agent.justHitByAgent2 = agent.gettingArrestedByAgent;
                        agent.healthBeforeKnockout = agent.health;
                        agent.deathMethod = "Arrested";
                        agent.deathKiller = agent.gettingArrestedByAgent.agentName;
                        agent.statusEffects.ChangeHealth(-200f);
                        agent.gettingArrestedByAgent.SetArrestingAgent(null);
                        agent.SetGettingArrestedByAgent(null);
                        agent.agentHitboxScript.SetWBSprites();
                        agent.StopInteraction();
                        goto start;
                    case Dead:
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        agent.statusEffects.ChangeHealth(-(agent.currentHealth - 1));
                        agent.statusEffects.ChangeHealth(-1);
                        goto start;
                    case Burned:
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        agent.deathMethod = "Fire";
                        // This avoids a magic number that would gib the agent.
                        agent.statusEffects.ChangeHealth(-(agent.currentHealth - 1));
                        agent.statusEffects.ChangeHealth(-1);
                        goto start;
                    case Gibbed:
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        agent.statusEffects.ChangeHealth(-200f);
                        goto start;
                    case KnockedOut:
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        agent.statusEffects.AddStatusEffect(VStatusEffect.Tranquilized);
                        agent.tranqTime = 1000;
                        goto start;
                    case Zombified:
                        agent.zombieWhenDead = true;
                        agent.GetHook<P_Agent_Hook>().SceneSetterFinished = true;
                        agent.statusEffects.ChangeHealth(-(agent.currentHealth - 1));
                        agent.statusEffects.ChangeHealth(-1);
                        goto start;
                }
            }
        }

        public static List<string> CustomGoalList(List<string> vanillaList) =>
            vanillaList
                .Concat(SceneSetters)
                .Concat(ActualGoals)
                .ToList();
    }
}