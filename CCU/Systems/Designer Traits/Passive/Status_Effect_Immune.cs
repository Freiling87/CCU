﻿using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Status_Effect_Immune : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Status_Effect_Immune>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is immune to all status effects... but they still have feelings."),
					[LanguageCode.Spanish] = "Este NPC es inmune a todo estatus, se imaginan como se aburre en las fiestas.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Status_Effect_Immune)),
					[LanguageCode.Spanish] = "Inmune a los Efectos",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.preventStatusEffects = true;
		}
	}
}