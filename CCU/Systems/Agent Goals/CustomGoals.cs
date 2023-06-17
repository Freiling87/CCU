using BepInEx.Logging;
using CCU.Hooks;
using CCU.Localization;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            Random_Patrol_Chunk = "Random Patrol (Chunk)", // New
            Random_Patrol_Map = "Random Patrol (Map)", // New
            Teleport_Public = "Random Teleport (Public)",
            Teleport_Private = "Random Teleport (Private)",
            Teleport_Prison = "Random Teleport (Private + Prison)",
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

            //  Legacy
            RandomTeleport = "Random Teleport", // Redirects to Teleport Public

            NoMoreSemicolon = "";

        public static List<string> SceneSetters_Active = new List<string>()
        {
            Arrested,
            Burned,
            Dead,
            Gibbed,
            KnockedOut,
            // RandomTeleport, // Legacy
            Teleport_Public,
            Zombified,
        };
        public static List<string> SceneSetters_All = new List<string>() 
        {
            Arrested,
            Burned,
            Dead,
            Gibbed,
            KnockedOut,
            Random_Patrol_Chunk,
            Random_Patrol_Map,
            RandomTeleport, // Legacy
            Teleport_Public,
            Zombified,
        };
        public static List<string> ActualGoals_Active = new List<string>()
        {
            //      Custom
            //  Investigate             //  Wander Agents + Wander Objects
            //  WanderAgents,           
            //  WanderAgentsNonOwners,  
            //  WanderAgentsOwners,
            //  WanderObjects,
            //  WanderObjectsOwned,

            //      Vanilla
            //  CommitArson,
            //  FleeDanger, // Probably not gonna work, and not really worth trying too hard on since it's unspecific.
            //  RobotClean,
        };

        public static void RunSceneSetters()
        {
            foreach (Agent agent in GC.agentList)
            {
                if (!SceneSetters_All.Contains(agent.defaultGoal) || agent.GetOrAddHook<H_Agent>().SceneSetterFinished)
                    continue;

                agent.ownerID = 99;

                switch (agent.defaultGoal)
                {
                    case Arrested:
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
                        break;

                    case Dead:
                        KillEmSoftly(agent);
                        break;

                    case Burned:
                        agent.deathMethod = "Fire";
                        KillEmSoftly(agent);
                        break;

                    case Gibbed:
                        agent.statusEffects.ChangeHealth(-200f);
                        break;

                    case KnockedOut:
                        agent.statusEffects.AddStatusEffect(VStatusEffect.Tranquilized);
                        agent.tranqTime = 1000;
                        break;

                    case RandomTeleport: // Legacy, Pre-Release
                        goto case Teleport_Public;

                    case Teleport_Public:
                        DoRandomTeleport(agent, false, true);
                        agent.SetDefaultGoal("WanderFar");
                        break;

                    case Zombified:
                        agent.zombieWhenDead = true;
                        KillEmSoftly(agent);
                        break;
                }
            }
        }

        private static void DoRandomTeleport(Agent agent, bool allowPrivate, bool allowPublic)
        {
            Vector3 targetLoc;
            int attempts = 0;

            do
            {
                targetLoc = GC.tileInfo.FindRandLocation(agent, allowPrivate, true);
                attempts++;
            }
            while (Vector2.Distance(targetLoc, agent.tr.position) < 8f && attempts < 50);

            if (targetLoc == Vector3.zero)
                targetLoc = agent.tr.position;

            agent.Teleport(targetLoc, false, true);
            try { agent.agentCamera.fastLerpTime = 1f; }
            catch { }
        }

        private static void KillEmSoftly(Agent agent)
        {
            // Kills without gibbing
            agent.statusEffects.ChangeHealth(-(agent.currentHealth - 1));
            agent.statusEffects.ChangeHealth(-1);
        }

        public static List<string> CustomGoalList(List<string> vanillaList) =>
            vanillaList
                .Concat(SceneSetters_Active)
                .Concat(ActualGoals_Active)
                .ToList();
    }
} 