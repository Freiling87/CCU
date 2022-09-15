using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Language
{
    public abstract class T_Language : T_PlayerTrait
    {
        public T_Language() : base() { }
        public abstract string[] VanillaSpeakers { get; }
    }
    public static class Language
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

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

		public static bool HaveSharedLanguage(Agent agent, Agent otherAgent)
		{
			List<T_Language> myLanguages = agent.GetTraits<T_Language>().ToList();
			List<T_Language> yourLanguages = otherAgent.GetTraits<T_Language>().ToList();

			logger.LogDebug("Mine (" + agent.agentRealName + ")");
			foreach (T_Language trait in myLanguages)
				logger.LogDebug(trait.TextName);

			logger.LogDebug("Yours (" + otherAgent.agentRealName + ")");
			foreach (T_Language trait in yourLanguages)
				logger.LogDebug(trait.TextName);

			List<string> sharedLangs = myLanguages.Select(myLang => myLang.TextName).Intersect(
				yourLanguages.Select(yourLang => yourLang.TextName)).ToList();

			logger.LogDebug("Intersect: ");
			foreach (string str in sharedLangs)
				logger.LogDebug(str);

			if (myLanguages.Select(myLang => myLang.TextName).Intersect(
				yourLanguages.Select(yourLang => yourLang.TextName)).Any())
				return true;

			return false;
		}

        [RLSetup]
		private static void Setup()
        {
			string t = NameTypes.Dialogue;

			RogueLibs.CreateCustomName("GibberishPlaceholder", t, new CustomNameInfo("I DON'T SPEAK YOUR LANGUAGE"));

			RogueLibs.CreateCustomName("Binary01", t, new CustomNameInfo("Bleep Bloop?"));
			RogueLibs.CreateCustomName("Binary02", t, new CustomNameInfo("ERR: Language.Intersect.Count == 0"));
			RogueLibs.CreateCustomName("Binary03", t, new CustomNameInfo("01001101 01100101 01100001 01110100 01100010 01100001 01100111!"));
			RogueLibs.CreateCustomName("Binary04", t, new CustomNameInfo("Fizz buzz? Buzz? Fizz? Buzz fizz?"));
			RogueLibs.CreateCustomName("Binary05", t, new CustomNameInfo("*Frustrated computing noises*"));

			RogueLibs.CreateCustomName("Chthonic01", t, new CustomNameInfo("*Backwards Rock Lyrics*"));
			RogueLibs.CreateCustomName("Chthonic02", t, new CustomNameInfo("Fhthgnan Zbuguluul"));
			RogueLibs.CreateCustomName("Chthonic03", t, new CustomNameInfo("..."));
			RogueLibs.CreateCustomName("Chthonic04", t, new CustomNameInfo("*Demonic groaning, but slower and louder*"));
			RogueLibs.CreateCustomName("Chthonic05", t, new CustomNameInfo("Gnuuuuuuhrg."));

			RogueLibs.CreateCustomName("ErSdtAdt01", t, new CustomNameInfo("y aIlMsi Smn cyI GAAamkDdl"));
			RogueLibs.CreateCustomName("ErSdtAdt02", t, new CustomNameInfo("nihaIeutetTd sMt nsneTko aeiT eR, fe Msa"));
			RogueLibs.CreateCustomName("ErSdtAdt03", t, new CustomNameInfo("a ala ls Ftnmuyr ErRgeEA Anegga"));
			RogueLibs.CreateCustomName("ErSdtAdt04", t, new CustomNameInfo("ERSDTADT... URUMI! *Gestures frustratedly*"));
			RogueLibs.CreateCustomName("ErSdtAdt05", t, new CustomNameInfo("YdPlC ettueuehW ilyaor'kcclsifaInolW Ire Tltlyea"));

			RogueLibs.CreateCustomName("Foreign01", t, new CustomNameInfo("Durka durka. Durka durka durk."));
			RogueLibs.CreateCustomName("Foreign02", t, new CustomNameInfo("Blorgle Blargle?"));
			RogueLibs.CreateCustomName("Foreign03", t, new CustomNameInfo("BLORGLE BLARGLE."));
			RogueLibs.CreateCustomName("Foreign04", t, new CustomNameInfo("No spee. Me no spee."));
			RogueLibs.CreateCustomName("Foreign05", t, new CustomNameInfo("I no unerstan. Sorreeee!"));

			RogueLibs.CreateCustomName("Goryllian01", t, new CustomNameInfo("Guh! Muh!"));
			RogueLibs.CreateCustomName("Goryllian02", t, new CustomNameInfo("Uh uh, ook ook. Ook."));
			RogueLibs.CreateCustomName("Goryllian03", t, new CustomNameInfo("*Haughty look* Ook."));
			RogueLibs.CreateCustomName("Goryllian04", t, new CustomNameInfo("Ooook. Me ooook. OOK! OOK?"));
			RogueLibs.CreateCustomName("Goryllian05", t, new CustomNameInfo("Ban. Mah. Ook oook. Ban mah!"));

			RogueLibs.CreateCustomName("Werewelsh01", t, new CustomNameInfo("GRRRRRRRR!!!"));
			RogueLibs.CreateCustomName("Werewelsh02", t, new CustomNameInfo("Groof! Groof!"));
			RogueLibs.CreateCustomName("Werewelsh03", t, new CustomNameInfo("*Excited, confused panting*"));
			RogueLibs.CreateCustomName("Werewelsh04", t, new CustomNameInfo("OOK! OOK?"));
			RogueLibs.CreateCustomName("Werewelsh05", t, new CustomNameInfo("Ban. Mah. Ook oook. Ban mah!"));
		}

		public static void SayGibberish(Agent agent)
        {
			agent.SayDialogue("GibberishPlaceholder");
        }
	}
}