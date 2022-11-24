using BepInEx.Logging;
using CCU.Localization;
using CCU.Patches.Agents;
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
            Teleport_Return_Idle = "Random Teleport + Return (Idle)", // New
            Teleport_Return_Patrol = "Random Teleport + Return (Patrol)", // New
            Teleport_Wander = "Random Teleport + Wander",
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
            RandomTeleport = "Random Teleport", // Redirects to Teleport + Wander

            NoMoreSemicolon = "";

        public static List<string> SceneSetters_Active = new List<string>()
        {
            Arrested,
            Burned,
            Dead,
            Gibbed,
            KnockedOut,
            Teleport_Wander,
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
            RandomTeleport,
            Teleport_Return_Idle,
            Teleport_Return_Patrol,
            Teleport_Wander,
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
        // TODO: Goto is a nono, mofo
        // It's structured like this so I can remove from agentList while traversing it.
        start: 
            foreach (Agent agent in GC.agentList)
            {
                if (!SceneSetters_All.Contains(agent.defaultGoal) || agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished)
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

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case Dead:
                        KillEmSoftly(agent);

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case Burned:
                        agent.deathMethod = "Fire";
                        KillEmSoftly(agent);

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case Gibbed:
                        agent.statusEffects.ChangeHealth(-200f);

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case KnockedOut:
                        agent.statusEffects.AddStatusEffect(VStatusEffect.Tranquilized);
                        agent.tranqTime = 1000;

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case RandomTeleport: 
                        goto case Teleport_Wander; // Legacy


                    case Teleport_Return_Idle:
                        DoRandomTeleport(agent);

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        break;


                    case Teleport_Wander:
                        DoRandomTeleport(agent);
                        agent.SetDefaultGoal("WanderFar");

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;


                    case Zombified:
                        agent.zombieWhenDead = true;
                        KillEmSoftly(agent);

                        agent.GetOrAddHook<P_Agent_Hook>().SceneSetterFinished = true;
                        goto start;
                }
            }
        }
        private static void DoRandomTeleport(Agent agent)
        {
            Vector3 targetLoc = Vector3.zero;
            int attempts = 0;

            do
            {
                targetLoc = GC.tileInfo.FindRandLocation(agent, true, true);
                attempts++;
            }
            while (Vector2.Distance(targetLoc, agent.tr.position) < 16f && attempts < 50);

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