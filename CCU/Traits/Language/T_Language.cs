using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Language
{
    public abstract class T_Language : T_PlayerTrait
    {
        public T_Language() : base() { }
        public abstract string[] VanillaSpeakers { get; }
    }
    public static class Language
    {
        public static void AddLangsToVanillaAgents(Agent agent)
        {
			switch (agent.agentName)
			{
				case VanillaAgents.Alien:
					agent.AddTrait<Speaks_ErSdtAdt>();
					break;
				case VanillaAgents.Assassin:
					agent.AddTrait<Speaks_Foreign>();
					break;
				case VanillaAgents.CopBot:
					agent.AddTrait<Speaks_Binary>();
					break;
				case VanillaAgents.Ghost:
					agent.AddTrait<Speaks_Chthonic>();
					break;
				case VanillaAgents.Gorilla:
					agent.AddTrait<Speaks_High_Goryllian>();
					break;
				case VanillaAgents.Hacker:
					agent.AddTrait<Speaks_Binary>();
					break;
				case VanillaAgents.KillerRobot:
					agent.AddTrait<Speaks_Binary>();
					break;
				case VanillaAgents.Robot:
					agent.AddTrait<Speaks_Binary>();
					break;
				case VanillaAgents.ShapeShifter:
					agent.AddTrait<Speaks_Chthonic>();
					break;
				case VanillaAgents.Vampire:
					agent.AddTrait<Speaks_Chthonic>();
					break;
				case VanillaAgents.Werewolf:
					agent.AddTrait<Speaks_Werewelsh>();
					break;
				case VanillaAgents.WerewolfTransformed:
					agent.AddTrait<Speaks_Werewelsh>();
					break;
				case VanillaAgents.Zombie:
					agent.AddTrait<Speaks_Chthonic>();
					break;
			}
		}
	}
}