using BepInEx.Logging;
using CCU.Traits;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(CharacterSheet))]
	public static class P_CharacterSheet
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(CharacterSheet.UpdateStats), argumentTypes: new Type[0] { })]
		public static bool UpdateStats_Prefix(CharacterSheet __instance, Agent ___agent, Text ___charText, StatusEffects ___statusEffects)
		{
			if (!GC.gameEventsStarted)
				return false;

			int num = GC.sessionData.skillLevel[___agent.isPlayer];
			int num2 = GC.sessionData.skillPoints[___agent.isPlayer];
			int num3 = ___agent.skillPoints.findLevelThreshold(num);
			string text = ___agent.agentRealName;

			if (___agent.possessing)
				text = GC.nameDB.GetName("ShapeShifter", "Agent");

			___charText.text = "";
			Text text2 = ___charText;

			text2.text = string.Concat(new object[]
			{
				text2.text,
				"<color=yellow>- ",
				GC.nameDB.GetName("Stats", "Interface"),
				" - </color>\n",
				text,
				"\n",
				GC.nameDB.GetName("Level", "Interface"),
				" ",
				num,
				"\n",
				GC.nameDB.GetName("SkillPoints", "Interface"),
				": ",
				num2,
				"/",
				num3,
				"\n\n"
			});

			text2 = ___charText;

			text2.text = string.Concat(new object[]
			{
				text2.text,
				GC.nameDB.GetName("Endurance", "Interface"),
				": ",
				___agent.enduranceStatMod + 1,
				"/4\n"
			});

			#region ATTRIBUTES
			if (___agent.statusEffects.hasTrait("RollerSkates") || ___agent.statusEffects.hasTrait("RollerSkates2"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Speed", "Interface"),
					": ",
					GC.nameDB.GetName("RollerSkates", "StatusEffect"),
					"\n"
				});
			}
			else if (___agent.statusEffects.hasTrait("BigCollider"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Speed", "Interface"),
					": ",
					GC.nameDB.GetName("BigCollider", "StatusEffect"),
					"\n"
				});
			}
			else
			{
				text2 = ___charText;

				text2.text = string.Concat(new object[]
				{
					text2.text,
					GC.nameDB.GetName("Speed", "Interface"),
					": ",
					___agent.speedStatMod + 1,
					"/4\n"
				});
			}

			if (___agent.statusEffects.hasTrait("CantAttack"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Strength", "Interface"),
					": ",
					GC.nameDB.GetName("CantAttack", "StatusEffect"),
					"\n"
				});
			}
			else if (___agent.statusEffects.hasTrait("AttacksOneDamage"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Strength", "Interface"),
					": ",
					GC.nameDB.GetName("AttacksOneDamage", "StatusEffect"),
					"\n"
				});
			}
			else
			{
				text2 = ___charText;

				text2.text = string.Concat(new object[]
				{
					text2.text,
					GC.nameDB.GetName("Strength", "Interface"),
					": ",
					___agent.strengthStatMod + 1,
					"/4\n"
				});
			}

			if (___agent.statusEffects.hasTrait("CantAttack"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Accuracy", "Interface"),
					": ",
					GC.nameDB.GetName("CantAttack", "StatusEffect"),
					"\n"
				});
			}
			else if (___agent.statusEffects.hasTrait("AttacksOneDamage"))
			{
				text2 = ___charText;

				text2.text = string.Concat(new string[]
				{
					text2.text,
					GC.nameDB.GetName("Accuracy", "Interface"),
					": ",
					GC.nameDB.GetName("AttacksOneDamage", "StatusEffect"),
					"\n"
				});
			}
			else
			{
				text2 = ___charText;

				text2.text = string.Concat(new object[]
				{
					text2.text,
					GC.nameDB.GetName("Accuracy", "Interface"),
					": ",
					___agent.accuracyStatMod + 1,
					"/4\n"
				});
			}
			#endregion

			if (___statusEffects.TraitList.Count != 0 || ___agent.inventory.equippedSpecialAbility != null)
			{
				#region STATUS EFFECTS
				if (___statusEffects.StatusEffectList.Count != 0)
				{
					Text text3 = ___charText;
					text3.text = text3.text + "\n<color=yellow>- " + GC.nameDB.GetName("StatusEffects", "Interface") + " - </color>\n";

					foreach (StatusEffect statusEffect in ___statusEffects.StatusEffectList)
					{
						text2 = ___charText;

						text2.text = string.Concat(new string[]
						{
							text2.text,
							"<color=cyan>",
							GC.nameDB.GetName(statusEffect.statusEffectName, "StatusEffect"),
							"</color>\n<color=white>",
							GC.nameDB.GetName(statusEffect.statusEffectName, "Description"),
							"</color>\n"
						});
					}
				}
				#endregion
				#region SPECIAL ABILITY
				if (___agent.oma.superSpecialAbility && ___agent.agentName != "Custom")
				{
					string str = ___agent.agentName;

					if (___agent.agentName == "Cop2")
						str = "Cop";
					else if (___agent.agentName == "Guard2")
						str = "Guard";
					else if (___agent.agentName == "UpperCruster")
						str = "Hobo";

					Text text4 = ___charText;
					text4.text = text4.text + "\n<color=yellow>- " + GC.nameDB.GetName("SuperSpecialAbilityAllcaps", "Interface") + " - </color>\n";
					Text text5 = ___charText;
					text5.text = text5.text + "<color=white>" + GC.nameDB.GetName("BQC_" + str, "Unlock") + "</color>\n";
				}

				if (___agent.inventory.equippedSpecialAbility != null)
				{
					Text text6 = ___charText;
					text6.text = text6.text + "\n<color=yellow>- " + GC.nameDB.GetName("SpecialAbility", "Interface") + " - </color>\n";
					text2 = ___charText;

					text2.text = string.Concat(new string[]
					{
						text2.text,
						"<color=cyan>",
						GC.nameDB.GetName(___agent.inventory.equippedSpecialAbility.invItemName, "Item"),
						"</color>\n<color=white>",
						GC.nameDB.GetName(___agent.inventory.equippedSpecialAbility.invItemName, "Description"),
						"</color>\n"
					});
				}
				#endregion

				bool flag = false;

				foreach (Trait trait in ___statusEffects.TraitList)
					if (trait.addedInGame && !___statusEffects.hasStatusEffect(trait.traitName))
					{
						flag = true;
						break;
					}

				#region EXTRA TRAITS
				if (flag)
				{
					Text text7 = ___charText;
					text7.text = text7.text + "\n<color=yellow>- " + GC.nameDB.GetName("ExtraTraits", "Interface") + " - </color>\n";

					foreach (Trait trait2 in ___statusEffects.TraitList)
					{
						bool hidden = false;

						if (!trait2.addedInGame)
							hidden = true;

						if (___statusEffects.hasStatusEffect(trait2.traitName))
							hidden = true;

						if (TraitManager.HiddenTraitNames.Contains(trait2.traitName)) // Remove Hidden Traits
							hidden = true;

						if (!hidden)
						{
							text2 = ___charText;

							text2.text = string.Concat(new string[]
							{
								text2.text,
								"<color=cyan>",
								GC.nameDB.GetName(trait2.traitName, "StatusEffect"),
								"</color>\n<color=white>",
								GC.nameDB.GetName(trait2.traitName, "Description"),
								"</color>\n"
							});
						}
					}
				}
				#endregion

				bool flag3 = false;

				if (___agent.possessing || ___agent.transforming)
				{
					using (List<Trait>.Enumerator enumerator2 = ___statusEffects.TraitList.GetEnumerator())
						while (enumerator2.MoveNext())
							if (!enumerator2.Current.addedInGame)
								flag3 = true;
				}

				#region STARTING TRAITS
				if ((!___agent.possessing && !___agent.transforming) || flag3)
				{
					Text text8 = ___charText;
					text8.text = text8.text + "\n<color=yellow>- " + GC.nameDB.GetName("StartingTraits", "Interface") + " - </color>\n";

					foreach (Trait trait3 in ___statusEffects.TraitList) 
					{
						bool traitHidden = false;

						if (trait3.addedInGame)
						{
							traitHidden = true;
							flag = true;
						}

						if (___statusEffects.hasStatusEffect(trait3.traitName))
							traitHidden = true;

						if (TraitManager.HiddenTraitNames.Contains(trait3.traitName)) // Remove Hidden traits
							traitHidden = true;

						if (!traitHidden)
						{
							text2 = ___charText;

							text2.text = string.Concat(new string[]
							{
								text2.text,
								"<color=cyan>",
								GC.nameDB.GetName(trait3.traitName, "StatusEffect"),
								"</color>\n<color=white>",
								GC.nameDB.GetName(trait3.traitName, "Description"),
								"</color>\n"
							});
						}
					}
				}
				#endregion
			}

			return false;
		}
	}
}
