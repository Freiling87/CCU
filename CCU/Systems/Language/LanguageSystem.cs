using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Networking;

namespace CCU.Systems.Language
{
	public class LanguageSystem : ISetupAgentStats
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		internal const string
			Binary = "Binary",
			Chthonic = "Chthonic",
			English = "English",
			ErSdtAdt = "ErSdtAdt",
			Foreign = "Foreign",
			Goryllian = "Goryllian",
			Undercant = "Undercant",
			Werewelsh = "Werewelsh",
			Suffix_NonEnglish = "_NonEnglish";

		internal static List<string> AllLanguages = new List<string>() { Binary, Chthonic, English, ErSdtAdt, Foreign, Goryllian, Undercant, Werewelsh };

		//	ISetupAgentStats
		public bool BypassUnlockChecks => true;
		public void SetupAgent(Agent agent)
		{
			logger.LogDebug("SetupAgent Language: " + agent.agentRealName);

			foreach (T_Language trait in CoreTools.AllClassesOfType<T_Language>())
			{

				if (trait.VanillaSpeakers.Contains(agent.agentName))
					agent.AddTrait(trait.GetType());
			}

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				agent.AddTrait<Speaks_English>();
		}

		[RLSetup]
		private static void Setup()
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
				[LanguageCode.English] = "*Frustrated gestures*",
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


			RogueLibs.CreateCustomName("Undercant01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Wets alda yabrin, y'mook?"
			});
			RogueLibs.CreateCustomName("Undercant02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Wombly gombly, erd?",
			});
			RogueLibs.CreateCustomName("Undercant03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Float on, Upsoider."
			});
			RogueLibs.CreateCustomName("Undercant04_NonEnglish", t, new CustomNameInfo
			{
				// Upside is mad bright, and all. My eyes are hurting all day.
				[LanguageCode.English] = "Upsoide mad broit, nall. M'glimpsies aalde ertin."
			});
			RogueLibs.CreateCustomName("Undercant05_NonEnglish", t, new CustomNameInfo
			{
				// 
				[LanguageCode.English] = "Make bes fer tip y'toes. D'Ungries moitcan fine y'smell."
			});


			RogueLibs.CreateCustomName("Werewelsh01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "GRRRRRRRR!!!",
				[LanguageCode.Spanish] = "Grrrrr GRRRRRR!!!",
			});
			RogueLibs.CreateCustomName("Werewelsh02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Shrug* Rough.",
				[LanguageCode.Spanish] = "GUAU!!! GUAU!!! GUAU!!!",
			});
			RogueLibs.CreateCustomName("Werewelsh03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Excited, confused panting*",
				[LanguageCode.Spanish] = "*Woof*",
			});
			RogueLibs.CreateCustomName("Werewelsh04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Idiotic head tilt*",
				[LanguageCode.Spanish] = "0-0?",
			});
			RogueLibs.CreateCustomName("Werewelsh05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Woof woof, woof.",
				[LanguageCode.Spanish] = "Roff Roff Roff, Roff Roff",
			});
		}

		// Tested, works
		public static bool HaveSharedLanguage(Agent agent, Agent otherAgent)
		{
			foreach (string lang in KnownLanguagesWithoutTranslator(agent, false).Intersect(KnownLanguagesWithoutTranslator(otherAgent, false)).ToList())
				logger.LogDebug("Shared Language : " + lang);

			return KnownLanguagesWithoutTranslator(agent, false).Intersect(KnownLanguagesWithoutTranslator(otherAgent, false)).ToList().Any();
		}

		public static string LanguageDialogueName(Agent agent)
		{
			if (agent.agentName is VanillaAgents.CustomCharacter)
			{
				List<string> spokenLangs = KnownLanguagesWithoutTranslator(agent, false);
				string language = CoreTools.GetRandomMember(spokenLangs) ?? "None";
				string number = UnityEngine.Random.Range(1, 5).ToString("D2");
				return language + number;
			}

			return agent.agentName;
		}

		public static List<string> KnownLanguagesWithoutTranslator(Agent agent, bool countTranslator)
		{
			if (countTranslator && agent.inventory.HasItem(VItemName.Translator)
				|| agent.HasTrait<Polyglot>())
				return AllLanguages;

			List<string> languages = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				languages.Add(English);

			return languages.Distinct().ToList();
		}

		public static void SayGibberish(Agent agent)
		{
			string dialogueName = LanguageDialogueName(agent) + Suffix_NonEnglish;
			logger.LogDebug("SayGibberish: " + dialogueName);
			agent.Say(GC.nameDB.GetName(dialogueName, NameTypes.Dialogue));
		}
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent_Language
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Agent.CanUnderstandEachOther))]
		private static IEnumerable<CodeInstruction> SetCanUnderstandEachOther(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_Agent_Language), nameof(P_Agent_Language.DontHaveSharedLanguage));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),		//	Agent
					new CodeInstruction(OpCodes.Ldarg_1),		//	OtherAgent
					new CodeInstruction(OpCodes.Call, customMethod),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Brfalse),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool DontHaveSharedLanguage(Agent instance, Agent otherAgent) =>
			!LanguageSystem.HaveSharedLanguage(instance, otherAgent);

		//	WARNING: This patch was inactive inside Language.cs. Reactivate at own risk :)
		[HarmonyTranspiler, HarmonyPatch(nameof(Agent.SayDialogue), new[] { typeof(bool), typeof(string), typeof(bool), typeof(NetworkInstanceId) })]
		private static IEnumerable<CodeInstruction> SetupAgentStats_LegacyUpdater(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo languageDialogueName = AccessTools.DeclaredMethod(typeof(LanguageSystem), nameof(LanguageSystem.LanguageDialogueName));

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
					new CodeInstruction(OpCodes.Ldstr, LanguageSystem.Suffix_NonEnglish)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(AgentInteractions))]
	public static class P_AgentInteractions_Language
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Do a second one for interactingagent
		[HarmonyTranspiler, HarmonyPatch(nameof(AgentInteractions.DetermineButtons))]
		private static IEnumerable<CodeInstruction> SoftcodeGorillaZombieInteraction_Agent(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo inventory = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.inventory));
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_AgentInteractions_Language), nameof(P_AgentInteractions_Language.HaveSharedLanguage));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),	//	Agent
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, inventory),
					new CodeInstruction(OpCodes.Ldstr, VanillaItems.Translator),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),	//	InteractingAgent
					new CodeInstruction(OpCodes.Call, customMethod),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		[HarmonyTranspiler, HarmonyPatch(nameof(AgentInteractions.DetermineButtons))]
		private static IEnumerable<CodeInstruction> SoftcodeGorillaZombieInteraction_InteractingAgent(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo inventory = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.inventory));
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_AgentInteractions_Language), nameof(P_AgentInteractions_Language.HaveSharedLanguage));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),	//	InteractingAgent
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, inventory),
					new CodeInstruction(OpCodes.Ldstr, VanillaItems.Translator),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),	//	Agent
					new CodeInstruction(OpCodes.Call, customMethod),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Leave for now in case this is inverted
		private static bool HaveSharedLanguage(Agent agent, Agent otherAgent) =>
			LanguageSystem.HaveSharedLanguage(agent, otherAgent);
	}

	[HarmonyPatch(typeof(InteractionHelper))]
	public static class P_InteractionHelper_Language
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(InteractionHelper.CanInteractWithAgent))]
		private static IEnumerable<CodeInstruction> LanguageGap(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(InteractionHelper), "agent");
			MethodInfo customDialogue = AccessTools.DeclaredMethod(typeof(P_InteractionHelper_Language), nameof(P_InteractionHelper_Language.LanguageGapDialogue));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, VanillaTraits.VocallyChallenged),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Pop),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1), // otherAgent
					new CodeInstruction(OpCodes.Call, customDialogue),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void LanguageGapDialogue(Agent agent, Agent otherAgent)
		{
			List<string> langs = LanguageSystem.KnownLanguagesWithoutTranslator(otherAgent, false);

			if (langs.Count > 1)
				agent.Say("I can't speak any languages they understand.");
			else if (langs.Count == 1)
				agent.Say("I can't speak " + langs[0] + ".");
			else if (langs.Count == 0)
				agent.Say("They seem to be nonverbal.");
		}
	}
}
