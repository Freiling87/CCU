using BepInEx.Logging;
using CCU.Localization;
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
            KnockedOut = "KnockedOut",

            //  Other Customs
            Panic = "Panic",
            WanderAgents = "WanderAgents",
            WanderAgentsNonOwners = "WanderAgentsNonOwners",
            WanderAgentsOwners = "WanderAgentsOwners",
            WanderObjects = "WanderObjects",
            WanderObjectsOwned = "WanderObjectsOwned",

            //  Vanilla Behaviors not normally available in editor
            CommitArson = "CommitArson",
            FleeDanger = "FleeDanger",
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
            CommitArson,
            FleeDanger, // Probably not gonna work, and not really worth trying too hard on since it's unspecific.
            RobotClean,
        };

        public static void RunSceneSetters(Agent agent)
        {
            if (!SceneSetters.Contains(agent.defaultGoal))
                return;

            // StatusEffects.ChangeHealth uses magic number -200f to auto-gib.
            // Some of the stuff below is to exploit or avoid that.
            switch (agent.defaultGoal)
            {
                case Arrested:
                    agent.agentInteractions.ArrestAgent(agent);
                    break;
                case Dead:
                    agent.statusEffects.ChangeHealth((agent.currentHealth + 1) * -1);
                    break;
                case Burned:
                    agent.deathMethod = "Fire"; // I think StatusEffects.SetupDeath will catch this and spawn the fire.
                    agent.statusEffects.ChangeHealth((agent.currentHealth + 1) * -1);
                    break;
                case Gibbed:
                    agent.statusEffects.ChangeHealth(-200f);
                    break;
                case KnockedOut:
                    agent.statusEffects.AddStatusEffect(VStatusEffect.Tranquilized);
                    agent.tranqTime = 1000;
                    // Hopefully SE.Update will pick this up.
                    break;
            }
        }

        public static List<string> CustomGoalList(List<string> vanillaList) =>
            vanillaList
                .Concat(SceneSetters)
                .Concat(ActualGoals)
                .ToList();
    }
}