using CCU.Patches.Agents;
using CCU.Traits;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace CCU.Items
{
    [ItemCategories(RogueCategories.Usable, RogueCategories.Technology)]
    public class ClassAWare : I_CCU, IItemTargetable
    {
        [RLSetup]
        public static void Setup()
        {
            ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<ClassAWare>()
                .WithName(new CustomNameInfo("Class-A-Ware"))
                .WithDescription(new CustomNameInfo("Shows info on any Agent. Gives a small XP bonus for discovering new classes. If used on an old class, the use is free but doesn't give XP."))
                .WithSprite(Properties.Resources.Classifier)
                .WithUnlock(new ItemUnlock
                {
                    UnlockCost = 10,
                    CharacterCreationCost = 1,
                    LoadoutCost = 2,
                });
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
        }

        public CustomTooltip TargetCursorText(PlayfieldObject target)
        {
            if (target is null || !(target is Agent))
                return "Seeking scan target...";
            else
                return "Valid target found:";
        }

        public bool TargetFilter(PlayfieldObject target) =>
            target is Agent agent;

        public bool TargetObject(PlayfieldObject target)
        {
            if (!TargetFilter(target)) 
                return false;

            Agent agent = (Agent)target;
            bool newClass = !Owner.GetOrAddHook<P_Agent_Hook>().ClassifierScannedAgents.Contains(agent.agentRealName);

            if (newClass)
            {
                Owner.GetOrAddHook<P_Agent_Hook>().ClassifierScannedAgents.Add(agent.agentRealName);
                Owner.skillPoints.AddPoints("HackPoints");
                Count--;
            }

            Item.invInterface.HideTarget();
            Owner.gc.audioHandler.Play(Owner, VanillaAudio.CopBotDetect);
            Owner.gc.spawnerMain.SpawnStateIndicator(agent, "HighVolume");
            CCULogger.GetLogger().LogDebug(MainText(agent, newClass, Owner));
            Owner.mainGUI.ShowBigImage(MainText(agent, newClass, Owner), "", null, target);

            return true;
        }

        private static string MainText(Agent agent, bool newClass, Agent owner)
        {
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
                "║</color><color=lime>\tScan complete. File saved: " + DossierFilename(agent) + " (" + DossierFilesize(agent) + " KB)</color><color=yellow>\n";
            else output +=
                "║</color><color=lime>\tLoading scan from memory: " + DossierFilename(agent) + " (" + DossierFilesize(agent) + " KB)...</color><color=yellow>\n";

            output +=
                "║</color><color=lime>\tMEM/CAP: " + TotalMemory(owner) + " KB / 16,384 KB</color><color=yellow>\n";

            int healthBarMaxBars = 33;
            int healthBarScale = (int)(agent.healthMax / healthBarMaxBars);

            output +=
                "╠╤══○ HEALTH\n" +
                "║├</color>" + ValueBar((int)agent.health / healthBarScale, healthBarMaxBars, 1) + "<color=yellow>\n" +
                "║└────•</color>[\t\t" + Math.Max(agent.health, 0) + "\t\t/\t\t" + agent.healthMax + "\t\t]<color=yellow>\n";

            if (agent.dead) output +=
                "║\t</color><color=red><< ERROR CODE 69707 - Target may be dead. >></color><color=yellow>\n";

            output +=
                "║\n";

            if (agent.specialAbility != "") output +=
                "╠╤══○ ABILITY\n" +
                "║└• </color>" + agent.specialAbility + "<color=yellow>\n";
            else output +=
                "╠═══○ ABILITY\n" +
                "║\t</color><color=red><< ERROR CODE 8008135 - Target has no talent. SAD! >></color>//<color=yellow>\n";

            output +=
                "║\n" +
                "╠╤══○ VITALS\n" +
                "║├• </color>Endurance\t\t\t\t" + ValueBar(agent.enduranceStatMod + 1, 4, 3) + "\t\t[ " + (agent.enduranceStatMod + 1) + " ]<color=yellow>\n" +
                "║├• </color>Speed\t\t\t\t\t\t" + ValueBar(agent.speedStatMod + 1, 4, 3) + "\t\t[ " + (agent.speedStatMod + 1) + " ]<color=yellow>\n" +
                "║├• </color>Melee\t\t\t\t\t\t" + ValueBar(agent.strengthStatMod + 1, 4, 3) + "\t\t[ " + (agent.strengthStatMod + 1) + " ]<color=yellow>\n" +
                "║└• </color>Firearms\t\t\t\t\t" + ValueBar(agent.accuracyStatMod + 1, 4, 3) + "\t\t[ " + (agent.accuracyStatMod + 1) + " ]<color=yellow>\n" +
                "║\n" +
                "╠╤══○ TRAITS\n";

            List<Trait> textList = T_CCU.PlayerTraitList(agent.statusEffects.TraitList).OrderBy(t => agent.gc.nameDB.GetName(t.traitName, "StatusEffect")).ToList();

            foreach (Trait trait in textList)
            {
                if (trait != textList[textList.Count - 1]) output +=
                    "║├• </color>" + agent.gc.nameDB.GetName(trait.traitName, "StatusEffect") + "<color=yellow>\n";
                else output +=
                    "║└• </color>" + agent.gc.nameDB.GetName(trait.traitName, "StatusEffect") + "<color=yellow>\n";
            }

            int breaks = 36 - Regex.Matches(output, "\n").Count;

            for (int i = 0; i < breaks; i++)
                output += "║\n";

            output +=
                "║\n" +
                "╚═════════○ Class-A-Ware MxII, v4.2.0\n" +
                "</color><color=lime>" +
                "By Cachet Biopharm, Copyright 2069. This device is not designed to\n" +
                "diagnose or treat medical conditions. Contains materials known in\n" +
                "the state of Neo-Cyber-California to cause Space Cancer.\n" +
                "</color><color=yellow>" +
                "○═════════════════════════════════════○</color>\n";

            return output;
        }

        private static string DossierFilename(Agent agent) => 
            agent.agentRealName.Replace(' ', '_') + ".cls";

        private static string DossierFilesize(Agent agent) =>
            String.Format("{0:n0}", agent.agentRealName.ToCharArray().Sum(x => x) % 100);

        private static string TotalMemory(Agent owner) =>
            "" + String.Format("{0:n0}", owner.GetOrAddHook<P_Agent_Hook>().ClassifierScannedAgents.Sum(n => n.ToCharArray().Sum(x => x) % 100));

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