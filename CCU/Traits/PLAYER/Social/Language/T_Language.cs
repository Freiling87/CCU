using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Hooks;
using CCU.Localization;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Player.Language
{
	public abstract class T_Language : T_PlayerTrait, IMutatorConditionalTrait
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public T_Language() : base() { }
        public abstract string[] VanillaSpeakers { get; }
		public abstract string[] LanguageNames { get; }

		public static void SetupLanguages(Agent agent)
		{
			if (agent.agentName != VanillaAgents.CustomCharacter)
			{
				List<T_Language> languageTraits = CoreTools.AllClassesOfType<T_Language>();

				foreach(T_Language trait in languageTraits)
					if (trait.VanillaSpeakers.Contains(agent.agentRealName))
						agent.AddTrait(trait.GetType());
			}

			agent.GetOrAddHook<H_Agent>().languages.Clear();
			agent.GetOrAddHook<H_Agent>().languages = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				agent.GetOrAddHook<H_Agent>().languages.Add(gc.nameDB.GetName(Language.English, NameTypes.Dialogue));
		}

		public bool IsAvailable()
		{
			// Make Language module dependent on Language challenge
			throw new NotImplementedException();
		}
	}

	public static class Language
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		private static void Setup()
		{
			SetupNames();
		}

		public const string
			Binary = "Binary",
			Chthonic = "Chthonic",
			English = "English",
			ErSdtAdt = "ErSdtAdt",
			Foreign = "Foreign",
			Goryllian = "Goryllian",
			Undercant = "Undercant",
			Werewelsh = "Werewelsh",

			z = "";

		private static void SetupNames()
		{
			string t = NameTypes.Dialogue;

			// Language Names
			RogueLibs.CreateCustomName(Binary, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Binary",
			});
			RogueLibs.CreateCustomName(Chthonic, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Chthonic",
			});
			RogueLibs.CreateCustomName(English, t, new CustomNameInfo
			{
				[LanguageCode.English] = "ErSdtAdt",
			});
			RogueLibs.CreateCustomName(ErSdtAdt, t, new CustomNameInfo
			{
				[LanguageCode.English] = "ErSdtAdt",
			});
			RogueLibs.CreateCustomName(Foreign, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Foreign",
			});
			RogueLibs.CreateCustomName(Goryllian, t, new CustomNameInfo
			{
				[LanguageCode.English] = "High_Goryllian",
			});
			RogueLibs.CreateCustomName(Undercant, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Undercant",
			});
			RogueLibs.CreateCustomName(Werewelsh, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Werewelsh",
			});

			// Gibberish dialogue, for each language
			// TODO: Split each language into its own class
			RogueLibs.CreateCustomName("Binary01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Bleep Bloop?",
			});
			RogueLibs.CreateCustomName("Binary02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "ERR: Lang.Intersect(someJerk) == 0\nAborting communication",
			});
			RogueLibs.CreateCustomName("Binary03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "01001101 01100101 01100001 01110100 01100010 01100001 01100111!",
			});
			RogueLibs.CreateCustomName("Binary04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Fizz buzz? Buzz? Fizz? Buzz fizz?",
			});
			RogueLibs.CreateCustomName("Binary05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated computing noises*",
			});

			RogueLibs.CreateCustomName("Chthonic01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Backwards Rock Lyrics*",
			});
			RogueLibs.CreateCustomName("Chthonic02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Fhthgnan Zbuguluul",
			});
			RogueLibs.CreateCustomName("Chthonic03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated demonic groaning*",
			});
			RogueLibs.CreateCustomName("Chthonic04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Demonic groaning, but slower and louder*",
			});
			RogueLibs.CreateCustomName("Chthonic05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ia! Ia! Ph’nglui mglw’nafh Cthulhu R’lyeh wgah’nagl fhtagn!",
			});

			RogueLibs.CreateCustomName("ErSdtAdt01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "y aIlMsi Smn cyI GAAamkDdl",
			});
			RogueLibs.CreateCustomName("ErSdtAdt02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "nihaIeutetTd sMt nsneTko aeiT eR, fe Msa",
			});
			RogueLibs.CreateCustomName("ErSdtAdt03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated telepathic vibrations*",
			});
			RogueLibs.CreateCustomName("ErSdtAdt04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "ERSDTADT... URUMI! *Gestures frustratedly*",
			});
			RogueLibs.CreateCustomName("ErSdtAdt05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "YdPlC ettueuehW ilyaor'kcclsifaInolW Ire Tltlyea",
			});

			RogueLibs.CreateCustomName("Foreign01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Durka durka! Durka durk durka!",
			});
			RogueLibs.CreateCustomName("Foreign02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Blorgle Blargle?",
			});
			RogueLibs.CreateCustomName("Foreign03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "BLORGLE BLARGLE.",
			});
			RogueLibs.CreateCustomName("Foreign04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "No spee. Me no spee.",
			});
			RogueLibs.CreateCustomName("Foreign05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated foreign noises*",
			});

			RogueLibs.CreateCustomName("Goryllian01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated grunting*",
			});
			RogueLibs.CreateCustomName("Goryllian02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Uh uh, ook ook. Ook.",
			});
			RogueLibs.CreateCustomName("Goryllian03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Haughty look* Ook.",
			});
			RogueLibs.CreateCustomName("Goryllian04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ooook. Me ooook. OOK! OOK?",
			});
			RogueLibs.CreateCustomName("Goryllian05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ban. Mah. Ook oook. Ban mah!",
			});

			// For characters without any spoken language
			RogueLibs.CreateCustomName("None01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Frustrated gestures*",
			});
			RogueLibs.CreateCustomName("None02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
			});
			RogueLibs.CreateCustomName("None03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
			});
			RogueLibs.CreateCustomName("None04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
			});
			RogueLibs.CreateCustomName("None05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.Chinese] = "...",
				[LanguageCode.English] = "...",
				[LanguageCode.Russian] = "...",
			});

			RogueLibs.CreateCustomName("Werewelsh01_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "GRRRRRRRR!!!"
			});
			RogueLibs.CreateCustomName("Werewelsh02_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Groof! Groof!"
			});
			RogueLibs.CreateCustomName("Werewelsh03_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Excited, confused panting*"
			});
			RogueLibs.CreateCustomName("Werewelsh04_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "*Idiotic head tilt*"
			});
			RogueLibs.CreateCustomName("Werewelsh05_NonEnglish", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Woof woof, woof."
			});
		}

		private static string SelectRandomDialogueName(Agent agent)
		{
			if (agent.agentName is VanillaAgents.CustomCharacter)
			{
				List<string> spokenLangs = LanguagesKnown(agent);
				string language = CoreTools.GetRandomMember(spokenLangs) ?? "None"; // For nonverbal characters
				string number = UnityEngine.Random.Range(1, 5).ToString("D2");

				return language + number;
			}

			return agent.agentName;
		}

		public static void SayGibberish(Agent agent)
		{
			agent.Say(GC.nameDB.GetName(SelectRandomDialogueName(agent) + "_NonEnglish", "Dialogue"));
		}

		private static bool HaveSharedLanguage(Agent agent, Agent otherAgent) =>
			LanguagesShared(agent, otherAgent).Any();

		public static List<string> LanguagesKnown(Agent agent)
		{
			if (agent.inventory.HasItem(vItem.Translator))
				return Polyglot.LanguagesStatic.ToList();

			List<string> languages = agent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (!agent.HasTrait(VanillaTraits.VocallyChallenged))
				languages.Add("English");

			return languages;
		}

		private static List<string> LanguagesShared(Agent agent, Agent otherAgent) =>
			LanguagesKnown(agent).Intersect(LanguagesKnown(otherAgent)).ToList();

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.CanUnderstandEachOther))]
		public static void AllowSharedLanguages(Agent __instance, Agent otherAgent, ref bool __result)
		{
			if (__result is false &&
				!__instance.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked) &&
				!otherAgent.statusEffects.hasStatusEffect(VStatusEffect.HearingBlocked) &&
				Language.LanguagesShared(__instance, otherAgent).Any())
				__result = true;

			return;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.SayDialogue))]
		private static IEnumerable<CodeInstruction> VaryGibberish(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo languageDialogueName = AccessTools.DeclaredMethod(typeof(Language), nameof(Language.SelectRandomDialogueName));

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

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons))]
		private static IEnumerable<CodeInstruction> BypassLanguageHardcode(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo canUnderstandEachOther = AccessTools.DeclaredMethod(typeof(Language), nameof(Language.HaveSharedLanguage));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, vItem.Translator),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Call, canUnderstandEachOther),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}