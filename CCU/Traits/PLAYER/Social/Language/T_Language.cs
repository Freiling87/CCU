using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Hooks;
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

		public static void SetupAgent(Agent agent)
		{
			if (agent.agentName != VanillaAgents.CustomCharacter)
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

			agent.GetOrAddHook<H_Agent>().languages.Clear();
			agent.GetOrAddHook<H_Agent>().languages = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				agent.GetOrAddHook<H_Agent>().languages.Add("English");
		}
	}

    public static class Language
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		private static void Setup()
		{
			SetupText();
		}

		private static void SetupText()
		{
			// Translators: These are meant to sound like foreign gibberish to an English speaker, for the various non-english languages in the game.
			string t = NameTypes.Dialogue;
			RogueLibs.CreateCustomName("Binary01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Bleep Bloop?",
                [LanguageCode.Spanish] = "*BROOOOM BROOOM BROOOM*",
            });
			RogueLibs.CreateCustomName("Binary02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "ERR: Lang.Intersect(someJerk) == 0\nAborting communication",
                [LanguageCode.Spanish] = "Error: Interacion Social ha sido rechazada. Mas Informacion. \nOk",
            });
			RogueLibs.CreateCustomName("Binary03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "01001101 01100101 01100001 01110100 01100010 01100001 01100111!",
                [LanguageCode.Spanish] = "01001110 01000101 01000101 01000101 01000101 01010010 01000100!",
            });
			RogueLibs.CreateCustomName("Binary04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Fizz buzz? Buzz? Fizz? Buzz fizz?",
                [LanguageCode.Spanish] = "BEEEEEP ?#$!*",
            });
			RogueLibs.CreateCustomName("Binary05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated computing noises*",
                [LanguageCode.Spanish] = "*Iiiiioooouuuu IIIUUUUOUU IIIIIIII*",
            });

			RogueLibs.CreateCustomName("Chthonic01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Backwards Rock Lyrics*",
                [LanguageCode.Spanish] = "*Bzt SENUBLASORTSEUNERPMOC Bzt*",
            });
			RogueLibs.CreateCustomName("Chthonic02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Fhthgnan Zbuguluul",
                [LanguageCode.Spanish] = "J'ghs Dnd C'nmlg'?",
            });
			RogueLibs.CreateCustomName("Chthonic03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated demonic groaning*",
                [LanguageCode.Spanish] = "Hoooooooooooooooo",
            });
			RogueLibs.CreateCustomName("Chthonic04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Demonic groaning, but slower and louder*",
                [LanguageCode.Spanish] = "*Grooooooooooooooo*",
            });
			RogueLibs.CreateCustomName("Chthonic05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ia! Ia! Ph’nglui mglw’nafh Cthulhu R’lyeh wgah’nagl fhtagn!",
                [LanguageCode.Spanish] = "Cl Cl Cl! 'Y'd m' Atrg'nth Lh L'nguh",
            });

			RogueLibs.CreateCustomName("ErSdtAdt01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "y aIlMsi Smn cyI GAAamkDdl",
                [LanguageCode.Spanish] = "Moooo?",
            });
			RogueLibs.CreateCustomName("ErSdtAdt02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "nihaIeutetTd sMt nsneTko aeiT eR, fe Msa",
                [LanguageCode.Spanish] = "aUnCn Em'slaEfT, tDmRoene icBnuOm",
            });
			RogueLibs.CreateCustomName("ErSdtAdt03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated telepathic vibrations*",
                [LanguageCode.Spanish] = "*Wrrrrrrrrr Zoop Zoop*",
            });
			RogueLibs.CreateCustomName("ErSdtAdt04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "ERSDTADT... URUMI! *Gestures frustratedly*",
                [LanguageCode.Spanish] = "Ueq pTaRe on'nTeEdNse! LaaEsm!!!",
            });
			RogueLibs.CreateCustomName("ErSdtAdt05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "YdPlC ettueuehW ilyaor'kcclsifaInolW Ire Tltlyea",
                [LanguageCode.Spanish] = "OeDnd sAeT sMeSi'ldoIo aOsVe iMSsm!!!",

            });

			RogueLibs.CreateCustomName("Foreign01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Durka durka! Durka durk durka!",
                [LanguageCode.Spanish] = "CheUstedsabeconqueinhablanomemireasicheesgrosero!",
            });
			RogueLibs.CreateCustomName("Foreign02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Blorgle Blargle?",
                [LanguageCode.Spanish] = "Qu'est ce que tu veux?",
            });
			RogueLibs.CreateCustomName("Foreign03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "BLORGLE BLARGLE.",
                [LanguageCode.Spanish] = "Che cose? coserenti?!",
            });
			RogueLibs.CreateCustomName("Foreign04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "No spee. Me no spee.",
                [LanguageCode.Spanish] = "Llo no sabele idiome.",
            });
			RogueLibs.CreateCustomName("Foreign05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated foreign noises*",
                [LanguageCode.Spanish] = "*La Frustacion Estranjera es Universal*",
            });

			RogueLibs.CreateCustomName("Goryllian01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated grunting*",
                [LanguageCode.Spanish] = "Gr! Uk! Uk! Uk!",
            });
			RogueLibs.CreateCustomName("Goryllian02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Uh uh, ook ook. Ook.",
                [LanguageCode.Spanish] = "Uh uk. Uk Uk. Uk.",
            });
			RogueLibs.CreateCustomName("Goryllian03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Haughty look* Ook.",
                [LanguageCode.Spanish] = "*Cof* Uk.",
            });
			RogueLibs.CreateCustomName("Goryllian04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ooook. Me ooook. OOK! OOK?",
                [LanguageCode.Spanish] = "Uk Uk. Bahuk. Uk Uk! Uk?",
            });
			RogueLibs.CreateCustomName("Goryllian05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ban. Mah. Ook oook. Ban mah!",
                [LanguageCode.Spanish] = "Bah, Nahmah Dah? Uk Uk. Bahnama!",
            });

			// For characters without any spoken language
			RogueLibs.CreateCustomName("None01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "*Frustrated gestures*",
                [LanguageCode.Spanish] = "*Frustacion Sordomuda*",
            });
			RogueLibs.CreateCustomName("None02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
                [LanguageCode.Spanish] = "...,",
            });
			RogueLibs.CreateCustomName("None03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
                [LanguageCode.Spanish] = "...",
            });
			RogueLibs.CreateCustomName("None04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
                [LanguageCode.Spanish] = "...!",

            });
			RogueLibs.CreateCustomName("None05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
                [LanguageCode.Spanish] = "...?",

            });

			RogueLibs.CreateCustomName("Werewelsh01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "GRRRRRRRR!!!"
                [LanguageCode.Spanish] = "Grrrrr GRRRRRR!!!",
            });
			RogueLibs.CreateCustomName("Werewelsh02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Groof! Groof!"
                [LanguageCode.Spanish] = "GUAU!!! GUAU!!! GUAU!!!",
            });
			RogueLibs.CreateCustomName("Werewelsh03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Excited, confused panting*"
                [LanguageCode.Spanish] = "*Woof*",
            });
			RogueLibs.CreateCustomName("Werewelsh04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Idiotic head tilt*"
                [LanguageCode.Spanish] = "0-0?",
            });
			RogueLibs.CreateCustomName("Werewelsh05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Woof woof, woof."
                [LanguageCode.Spanish] = "Roff Roff Roff, Roff Roff",
            });
		}

		internal static string LanguageDialogueName(Agent agent)
		{
			if (agent.agentName is VanillaAgents.CustomCharacter)
			{
				List<string> spokenLangs = GetLanguages(agent);
				string language = CoreTools.GetRandomMember(spokenLangs) ?? "None";
				string number = UnityEngine.Random.Range(1, 5).ToString("D2");

				return language + number;
			}

			return agent.agentName;
		}

		internal static void SayGibberish(Agent agent)
		{
			agent.Say(GC.nameDB.GetName(LanguageDialogueName(agent) + "_NonEnglish", "Dialogue"));
		}

		internal static List<string> GetLanguages(Agent agent)
		{
			if (agent.inventory.HasItem(vItem.Translator))
				return Polyglot.LanguagesStatic.ToList();

			List<string> languages = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				languages.Add("English");

			return languages;
		}

		public static bool HaveSharedLanguage(Agent agent, Agent otherAgent) =>
			SharedLanguages(agent, otherAgent).Any();

		public static List<string> SharedLanguages(Agent agent, Agent otherAgent) =>
			GetLanguages(agent).Intersect(GetLanguages(otherAgent)).ToList();

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
	}
}