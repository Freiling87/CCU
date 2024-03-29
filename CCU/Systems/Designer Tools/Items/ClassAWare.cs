﻿using BepInEx.Logging;
using CCU.Tools;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CCU.Items
{
	[ItemCategories(RogueCategories.Usable, RogueCategories.Technology)]
	public class ClassAWare : I_CCU, IItemTargetable
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	TODO: Set up an HTML markup manager. E.g., ColoredText(texthere, yellow), which outputs with enclosing brackets

		[RLSetup]
		public static void Setup()
		{
			ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<ClassAWare>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Class-A-Ware",
					[LanguageCode.Spanish] = "Clasista-Inador",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Shows a full stat readout of a target NPC. Gives bonus XP for each new NPC scanned. Free use on previously scanned classes.",
					[LanguageCode.Spanish] = "Muestra toda la información de un NPC, da un poco de experiencia al detectar NPCs por la primera ves y no pierde usos en NPCs repetidos.",
				})
				.WithSprite(Properties.Resources.ClassAWare)
				.WithUnlock(new ItemUnlock
				{
					CharacterCreationCost = 1,
					IsAvailable = true,
					IsAvailableInItemTeleporter = true,
					IsEnabled = true, // Show up as quest reward?
					LoadoutCost = 1,
					UnlockCost = 10,
				});

			RogueLibs.CreateCustomAudio("ClassAWare_Use", Properties.Resources.ClassAware_Use, AudioType.WAV);
		}

		public override void SetupDetails()
		{
			Item.goesInToolbar = true;
			Item.hasCharges = true;
			Item.itemType = ItemTypes.Tool;
			Item.itemValue = 20;
			Item.initCount = 3;
			Item.rewardCount = 3;
			Item.stackable = true;
			Item.Categories = new List<string> { VItemCategory.Technology, VItemCategory.Usable };
		}

		public static CustomNameInfo targetingText = new CustomNameInfo
		{
			[LanguageCode.English] = "Seeking scan target...",
			[LanguageCode.Spanish] = "Buscando NPC elegible...",
		};
		public static CustomNameInfo validTargetText = new CustomNameInfo
		{
			[LanguageCode.English] = "Valid target found:",
			[LanguageCode.Spanish] = "NPC valido encontrado:",
		};
		public CustomTooltip TargetCursorText(PlayfieldObject target)
		{
			if (target is null || !(target is Agent))
				return targetingText;
			else
				return validTargetText;
		}

		public bool TargetFilter(PlayfieldObject target) =>
			target is Agent agent;

		public bool TargetObject(PlayfieldObject target)
		{
			if (!TargetFilter(target))
				return false;

			Agent agent = (Agent)target;
			bool newClass = !Owner.GetOrAddHook<H_AgentInteractions>().classawareStoredAgents.Contains(agent.agentRealName);

			if (newClass)
			{
				Owner.GetOrAddHook<H_AgentInteractions>().classawareStoredAgents.Add(agent.agentRealName);
				Owner.skillPoints.AddPoints("HackPoints");
				Count--;
			}

			Item.invInterface.HideTarget();
			Owner.gc.audioHandler.Play(Owner, "ClassAWare_Use");
			Owner.gc.spawnerMain.SpawnStateIndicator(agent, "HighVolume");
			Owner.mainGUI.ShowBigImage(MainText(agent, newClass, Owner), "", null, target);
			Logging.DoAllLogging(agent);
			return true;
		}

		#region Header
		static CustomName headerText1 = RogueLibs.CreateCustomName("headerText1", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Scan complete. File saved:",
			[LanguageCode.Spanish] = "Escaneo completado. Archivo guardado:",
		});
		static CustomName headerText2 = RogueLibs.CreateCustomName("headerText2", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Loading scan from memory:",
			[LanguageCode.Spanish] = "Cargando escaneo guardado:",
		});
		// "Memory / Capacity"
		static CustomName headerText3 = RogueLibs.CreateCustomName("headerText3", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "MEM/CAP",
			[LanguageCode.Spanish] = "MEM/CAP",
		});
		#endregion
		#region Health
		static CustomName healthText1 = RogueLibs.CreateCustomName("healthText1", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "HEALTH",
			[LanguageCode.Spanish] = "SALUD",
		});
		static CustomName healthTextDead = RogueLibs.CreateCustomName("healthTextDead", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "ERROR CODE 69707 - Target may be dead.",
			[LanguageCode.Spanish] = "ERROR 33413 - Es posible que este muerto.",
		});
		#endregion
		#region Special Ability
		static CustomName abilityText1 = RogueLibs.CreateCustomName("abilityText1", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "ABILITY",
			[LanguageCode.Spanish] = "ABILIDAD",
		});
		static CustomName abilityNoneFound = RogueLibs.CreateCustomName("abilityNoneFound", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "ERROR CODE 8008135 - Target has no talent. SAD!",
			[LanguageCode.Spanish] = "ERROR 518008 - Talento no encontrado. Normie.",
		});
		#endregion
		#region Attributes
		// Translators: Please adjust number of tabs ("\t") to align attribute bars evenly
		static CustomName attributeHeader = RogueLibs.CreateCustomName("attributeHeader", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "VITALS",
			[LanguageCode.Spanish] = "VITALES",
		});
		static CustomName attributeName1 = RogueLibs.CreateCustomName("attributeName1", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Endurance\t\t\t\t",
			[LanguageCode.Spanish] = "Resistencia\t\t\t\t",
		});
		static CustomName attributeName2 = RogueLibs.CreateCustomName("attributeName2", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Speed\t\t\t\t\t\t",
			[LanguageCode.Spanish] = "Velocidad\t\t\t\t\t\t",
		});
		static CustomName attributeName3 = RogueLibs.CreateCustomName("attributeName3", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Melee\t\t\t\t\t\t",
			[LanguageCode.Spanish] = "Fuerza\t\t\t\t\t\t",
		});
		static CustomName attributeName4 = RogueLibs.CreateCustomName("attributeName4", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Firearms\t\t\t\t\t",
			[LanguageCode.Spanish] = "Puntería\t\t\t\t\t\t",
		});
		#endregion
		#region Traits
		static CustomName traitsHeader = RogueLibs.CreateCustomName("traitsHeader", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "TRAITS",
			[LanguageCode.Spanish] = "RASGOS",
		});
		static CustomName noTraitsText = RogueLibs.CreateCustomName("noTraitsText", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "ERROR CODE 0000 - No identifiable traits. LOSER ALERT!",
			[LanguageCode.Spanish] = "ERROR 1107 - Ningun rasgo detectado, Que diseño MEDIOCRE",
		});
		#endregion
		#region Footer
		static CustomName footerText1 = RogueLibs.CreateCustomName("footerText1", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "Class-A-Ware MxII, v4.2.0",
			[LanguageCode.Spanish] = "Clasistator MarxII v1.98.4",
		});
		// The following 3 strings are all part of the same text block.
		static CustomName footerText2 = RogueLibs.CreateCustomName("footerText2", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "By Cachet Biopharm, Copyright 2069. This device is not designed to",
			[LanguageCode.Spanish] = "Por Mixen Vanilla, Derechos Reservados 1944, No usar en",
		});
		static CustomName footerText3 = RogueLibs.CreateCustomName("footerText3", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "diagnose or treat medical conditions. Contains materials known in",
			[LanguageCode.Spanish] = "diagnosticos medicos o turismo. Contiene quimicos no aptos para",
		});
		static CustomName footerText4 = RogueLibs.CreateCustomName("footerText4", "Interface", new CustomNameInfo
		{
			[LanguageCode.English] = "the state of Neo-Cyber-California to cause Space Cancer.",
			[LanguageCode.Spanish] = "consumo infantil de prejudicios o conocimiento real.",
		});
		#endregion

		private static string MainText(Agent agent, bool newClass, Agent owner)
		{
			#region Header
			string output =
				"<color=cyan>" +
				" ╱╱╱╱╭━┳╮╭━━┳━━┳━━╮╱╱╱╱╭━━╮╱╱╱╱╭┳━┳┳━━┳━┳━╮╱╱ TM\n" +
				" ╱╱╱╱┃╭┫┃┃╭╮┃━━┫━━┫╭━━╮┃╭╮┃╭━━╮┃┃┃┃┃╭╮┃╋┃┳╯╱╱╱╱\n" +
				" ╱╱╱╱┃╰┫╰┫┣┫┣━━┣━━┃╰━━╯┃┣┫┃╰━━╯┃┃┃┃┃┣┫┃╮┫┻╮╱╱╱╱\n" +
				" ╱╱╱╱╰━┻━┻╯╰┻━━┻━━╯╱╱╱╱╰╯╰╯╱╱╱╱╰━┻━┻╯╰┻┻┻━╯╱╱╱╱</color>\n\n";

			output +=
				"<color=yellow>" +
				"╔═════════════════════════════════════○\n";

			if (newClass) output +=
				"║</color><color=lime>\t" + GC.nameDB.GetName(nameof(headerText1), "Interface") + " " + DossierFilename(agent) + " (" + DossierFilesize(agent) + " KB)</color><color=yellow>\n";
			else output +=
				"║</color><color=lime>\t" + GC.nameDB.GetName(nameof(headerText2), "Interface") + " " + DossierFilename(agent) + " (" + DossierFilesize(agent) + " KB)...</color><color=yellow>\n";

			output +=
				"║</color><color=lime>\t" + GC.nameDB.GetName(nameof(headerText3), "Interface") + " " + TotalMemory(owner) + " KB / 16,384 KB</color><color=yellow>\n";
			#endregion

			#region Health
			int healthBarMaxBars = 33;
			int healthBarScale = Math.Max((int)(agent.healthMax / healthBarMaxBars), 1);

			output +=
				"╠╤══○ " + GC.nameDB.GetName(nameof(healthText1), "Interface") + "\n" +
				"║├</color>" + ValueBar((int)(agent.health / healthBarScale), healthBarMaxBars, 1) + "<color=yellow>\n" +
				"║└────•</color>[\t\t" + Math.Max(agent.health, 0) + "\t\t/\t\t" + agent.healthMax + "\t\t]<color=yellow>\n";

			if (agent.dead) output +=
				"║\t</color><color=red><< " + GC.nameDB.GetName(nameof(healthTextDead), "Interface") + " >></color><color=yellow>\n";

			output +=
				"║\n";
			#endregion

			#region Special Ability
			if (agent.specialAbility != "") output +=
				"╠╤══○ " + GC.nameDB.GetName(nameof(abilityText1), "Interface") + "\n" +
				"║└• </color>" + agent.specialAbility + "<color=yellow>\n";
			else output +=
				"╠═══○ " + GC.nameDB.GetName(nameof(abilityText1), "Interface") + "\n" +
				"║\t</color><color=red><< " + GC.nameDB.GetName(nameof(abilityNoneFound), "Interface") + " >></color><color=yellow>\n";
			#endregion

			#region Attributes
			output +=
				"║\n" +
				"╠╤══○ " + GC.nameDB.GetName(nameof(attributeHeader), "Interface") + "\n" +
				"║├• </color>" + GC.nameDB.GetName(nameof(attributeName1), "Interface") + ValueBar(agent.enduranceStatMod + 1, 4, 3) + "\t\t[ " + (agent.enduranceStatMod + 1) + " ]<color=yellow>\n" +
				"║├• </color>" + GC.nameDB.GetName(nameof(attributeName2), "Interface") + ValueBar(agent.speedStatMod + 1, 4, 3) + "\t\t[ " + (agent.speedStatMod + 1) + " ]<color=yellow>\n" +
				"║├• </color>" + GC.nameDB.GetName(nameof(attributeName3), "Interface") + ValueBar(agent.strengthStatMod + 1, 4, 3) + "\t\t[ " + (agent.strengthStatMod + 1) + " ]<color=yellow>\n" +
				"║└• </color>" + GC.nameDB.GetName(nameof(attributeName4), "Interface") + ValueBar(agent.accuracyStatMod + 1, 4, 3) + "\t\t[ " + (agent.accuracyStatMod + 1) + " ]<color=yellow>\n" +
				"║\n";
			#endregion

			#region Traits
			List<Trait> playerTraitList = agent.statusEffects.TraitList.Where(t => !TraitManager.IsDesignerTrait(t)).OrderBy(t => agent.gc.nameDB.GetName(t.traitName, NameTypes.StatusEffect)).ToList();

			if (playerTraitList.Count() == 0)
			{
				output +=
					"╠═══○ " + GC.nameDB.GetName(nameof(traitsHeader), "Interface") + "\n" +
					"║\t</color><color=red><< " + GC.nameDB.GetName(nameof(noTraitsText), "Interface") + " >></color><color=yellow>\n";
			}
			else
			{
				output +=
					"╠╤══○ " + GC.nameDB.GetName(nameof(traitsHeader), "Interface") + "\n";

				foreach (Trait trait in playerTraitList)
				{
					if (trait != playerTraitList[playerTraitList.Count - 1]) output +=
						"║├• </color>" + agent.gc.nameDB.GetName(trait.traitName, "StatusEffect") + "<color=yellow>\n";
					else output +=
						"║└• </color>" + agent.gc.nameDB.GetName(trait.traitName, "StatusEffect") + "<color=yellow>\n";
				}
			}
			#endregion

			int height = 36 - Regex.Matches(output, "\n").Count;

			for (int i = 0; i < height; i++)
				output += "║\n";

			output +=
				"║\n" +
				"╚═════════○ " + GC.nameDB.GetName(nameof(footerText1), "Interface") + "\n" +
				"</color><color=lime>" +
				GC.nameDB.GetName(nameof(footerText2), "Interface") + "\n" +
				GC.nameDB.GetName(nameof(footerText3), "Interface") + "\n" +
				GC.nameDB.GetName(nameof(footerText4), "Interface") + "\n" +
				"</color><color=yellow>" +
				"○═════════════════════════════════════○</color>\n";

			return output;
		}

		private static string DossierFilename(Agent agent) =>
			agent.agentRealName.Replace(' ', '_') + ".cls";

		private static string DossierFilesize(Agent agent) =>
			String.Format("{0:n0}", agent.agentRealName.ToCharArray().Sum(x => x) % 100);

		private static string TotalMemory(Agent owner) =>
			"" + String.Format("{0:n0}", owner.GetOrAddHook<H_AgentInteractions>().classawareStoredAgents.Sum(n => n.ToCharArray().Sum(x => x) % 100));

		private static string ValueBar(int value, int max, int scale)
		{
			string output = "<color=lime>•";

			for (int i = 0; i < Math.Min(value, max) * scale; i++)
				output += "▓";

			if (value < max)
			{
				output += "</color><color=red>";

				for (int i = 0; i < (max - value) * scale; i++)
					output += "▓";
			}

			output += "•</color>";

			return output;
		}
	}
}