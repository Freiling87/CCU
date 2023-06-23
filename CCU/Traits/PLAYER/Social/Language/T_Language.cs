using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Player.Language
{
    public abstract class T_Language : T_PlayerTrait
    {
        public T_Language() : base() { }
        public abstract string[] VanillaSpeakers { get; }
		public abstract string[] LanguageNames { get; }
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
			if (agent.HasTrait<Polyglot>() || otherAgent.HasTrait<Polyglot>() ||
				agent.inventory.HasItem(vItem.Translator) || otherAgent.inventory.HasItem(vItem.Translator) ||
				(!agent.HasTrait(VanillaTraits.VocallyChallenged) && !otherAgent.HasTrait(VanillaTraits.VocallyChallenged)))
				return true;

			List<T_Language> myLanguages = agent.GetTraits<T_Language>().ToList();
			List<T_Language> yourLanguages = otherAgent.GetTraits<T_Language>().ToList();

			List<string> sharedLangs = myLanguages.Select(myLang => myLang.TextName).Intersect( 
				yourLanguages.Select(yourLang => yourLang.TextName)).ToList();

			foreach (string str in sharedLangs)
				logger.LogDebug("Shared Language: " + str);

			if (sharedLangs.Any())
				return true;

			return false;
		}

        [RLSetup]
		private static void Setup()
        {
			string t = NameTypes.Dialogue;

			RogueLibs.CreateCustomName("Binary01_NonEnglish", t, new CustomNameInfo("Bleep Bloop?"));
			RogueLibs.CreateCustomName("Binary02_NonEnglish", t, new CustomNameInfo("ERR: Lang.Intersect(someJerk) == 0\nAborting communication"));
			RogueLibs.CreateCustomName("Binary03_NonEnglish", t, new CustomNameInfo("01001101 01100101 01100001 01110100 01100010 01100001 01100111!"));
			RogueLibs.CreateCustomName("Binary04_NonEnglish", t, new CustomNameInfo("Fizz buzz? Buzz? Fizz? Buzz fizz?"));
			RogueLibs.CreateCustomName("Binary05_NonEnglish", t, new CustomNameInfo("*Frustrated computing noises*"));

			RogueLibs.CreateCustomName("Chthonic01_NonEnglish", t, new CustomNameInfo("*Backwards Rock Lyrics*"));
			RogueLibs.CreateCustomName("Chthonic02_NonEnglish", t, new CustomNameInfo("Fhthgnan Zbuguluul"));
			RogueLibs.CreateCustomName("Chthonic03_NonEnglish", t, new CustomNameInfo("..."));
			RogueLibs.CreateCustomName("Chthonic04_NonEnglish", t, new CustomNameInfo("*Demonic groaning, but slower and louder*"));
			RogueLibs.CreateCustomName("Chthonic05_NonEnglish", t, new CustomNameInfo("Gnuuuuuuhrg."));

			RogueLibs.CreateCustomName("ErSdtAdt01_NonEnglish", t, new CustomNameInfo("y aIlMsi Smn cyI GAAamkDdl"));
			RogueLibs.CreateCustomName("ErSdtAdt02_NonEnglish", t, new CustomNameInfo("nihaIeutetTd sMt nsneTko aeiT eR, fe Msa"));
			RogueLibs.CreateCustomName("ErSdtAdt03_NonEnglish", t, new CustomNameInfo("a ala ls Ftnmuyr ErRgeEA Anegga"));
			RogueLibs.CreateCustomName("ErSdtAdt04_NonEnglish", t, new CustomNameInfo("ERSDTADT... URUMI! *Gestures frustratedly*"));
			RogueLibs.CreateCustomName("ErSdtAdt05_NonEnglish", t, new CustomNameInfo("YdPlC ettueuehW ilyaor'kcclsifaInolW Ire Tltlyea"));

			RogueLibs.CreateCustomName("Foreign01_NonEnglish", t, new CustomNameInfo("Durka durka! Durka durk durka!"));
			RogueLibs.CreateCustomName("Foreign02_NonEnglish", t, new CustomNameInfo("Blorgle Blargle?"));
			RogueLibs.CreateCustomName("Foreign03_NonEnglish", t, new CustomNameInfo("BLORGLE BLARGLE."));
			RogueLibs.CreateCustomName("Foreign04_NonEnglish", t, new CustomNameInfo("No spee. Me no spee."));
			RogueLibs.CreateCustomName("Foreign05_NonEnglish", t, new CustomNameInfo("I no unerstan. Sorreeee!"));

			RogueLibs.CreateCustomName("Goryllian01_NonEnglish", t, new CustomNameInfo("Guh! Muh!"));
			RogueLibs.CreateCustomName("Goryllian02_NonEnglish", t, new CustomNameInfo("Uh uh, ook ook. Ook."));
			RogueLibs.CreateCustomName("Goryllian03_NonEnglish", t, new CustomNameInfo("*Haughty look* Ook."));
			RogueLibs.CreateCustomName("Goryllian04_NonEnglish", t, new CustomNameInfo("Ooook. Me ooook. OOK! OOK?"));
			RogueLibs.CreateCustomName("Goryllian05_NonEnglish", t, new CustomNameInfo("Ban. Mah. Ook oook. Ban mah!"));

			RogueLibs.CreateCustomName("Werewelsh01_NonEnglish", t, new CustomNameInfo("GRRRRRRRR!!!"));
			RogueLibs.CreateCustomName("Werewelsh02_NonEnglish", t, new CustomNameInfo("Groof! Groof!"));
			RogueLibs.CreateCustomName("Werewelsh03_NonEnglish", t, new CustomNameInfo("*Excited, confused panting*"));
			RogueLibs.CreateCustomName("Werewelsh04_NonEnglish", t, new CustomNameInfo("*Idiotic head tilt*"));
			RogueLibs.CreateCustomName("Werewelsh05_NonEnglish", t, new CustomNameInfo("Woof."));
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.SayDialogue))]
		private static IEnumerable<CodeInstruction> SetupAgentStats_LegacyUpdater(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo languageDialogueName = AccessTools.DeclaredMethod(typeof(Language), nameof(Language.LanguageDialogueName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, languageDialogueName),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{ 
					new CodeInstruction(OpCodes.Ldstr, "_NonEnglish")
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static string LanguageDialogueName(Agent agent)
		{
			if (agent.agentName is VanillaAgents.CustomCharacter)
			{
				List<string> spokenLangs = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

				string language = CoreTools.GetRandomMember(spokenLangs);
				string number = UnityEngine.Random.Range(1, 5).ToString("D2");

				return language + number;
			}

			return agent.agentName;
		}

		public static void SayGibberish(Agent agent)
		{
			agent.Say(GC.nameDB.GetName(LanguageDialogueName(agent) + "_NonEnglish", "Dialogue"));
		}
	}
}