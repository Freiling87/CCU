using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Identify : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.Identify;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => VDetermineMoneyCost.IdentifySyringe;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Identify>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can identify objects for money."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Identify)),

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
		
		
	}

	[HarmonyPatch(typeof(AgentInteractions))]
	internal static class P_AgentInteractions
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(AgentInteractions.UseItemOnObject))]
		public static bool UseItemOnObject_Prefix(Agent agent, Agent interactingAgent, InvItem item, string combineType, string useOnType, ref bool __result)
		{
			if (useOnType == VButtonText.Identify && agent.agentName == VanillaAgents.CustomCharacter)
			{
				if (item.invItemName == VItemName.Syringe)
				{
					if (combineType == "Combine")
					{
						if (!GC.syringesIdentified.Contains(item.contents[0]) && !interactingAgent.statusEffects.hasTrait(VanillaTraits.Drugalug))
						{
							if (agent.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.IdentifySyringe)))
							{
								agent.SayDialogue("Bought2");
								interactingAgent.statusEffects.IdentifySyringe(item.contents[0]);
							}
						}
						else
						{
							interactingAgent.SayDialogue("AlreadyIdentified");
							GC.audioHandler.Play(interactingAgent, "CantDo");
						}
					}

					__result = true;
					return false;
				}

				__result = false;
				return false;
			}

			return true;
		}
	}
}