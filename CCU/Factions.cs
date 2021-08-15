using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx;
using UnityEngine;
using RogueLibsCore;
using BepInEx.Logging;

namespace CCU
{
	public class Factions 
	{
        public void Awake()
        {
            RogueLibs.LoadFromAssembly();

            patcher.Prefix(typeof(Relationships), nameof(Relationships.SetupRelationshipOriginal), new Type[1] { typeof(Agent) });


        }

        public static bool Relationships_SetupRelationshipOriginal(Agent otherAgent, Relationships __instance, Agent ___agent)
        {
            if ((___agent.HasTrait(cTrait.Faction1) && otherAgent.HasTrait(cTrait.Faction1)) ||
                (___agent.HasTrait(cTrait.Faction2) && otherAgent.HasTrait(cTrait.Faction2)) ||
                (___agent.HasTrait(cTrait.Faction3) && otherAgent.HasTrait(cTrait.Faction3)) ||
                (___agent.HasTrait(cTrait.Faction4) && otherAgent.HasTrait(cTrait.Faction4)) ||
                (___agent.HasTrait(cTrait.Faction5) && otherAgent.HasTrait(cTrait.Faction5)) ||
                (___agent.HasTrait(cTrait.Faction6) && otherAgent.HasTrait(cTrait.Faction6)) ||
                (___agent.HasTrait(cTrait.Faction7) && otherAgent.HasTrait(cTrait.Faction7)) ||
                (___agent.HasTrait(cTrait.Faction8) && otherAgent.HasTrait(cTrait.Faction8)))
			{
                __instance.SetRelInitial(otherAgent, "Aligned");
                otherAgent.relationships.SetRelInitial(___agent, "Aligned");

                return false;
			}




            return true;
        }
        public static bool LevelEditor_Update(LevelEditor __instance)
        {

            return true;
        }

    }
}
