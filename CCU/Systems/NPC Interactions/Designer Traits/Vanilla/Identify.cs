using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Identify : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.Identify;
		public override string MoneyCostName => VDetermineMoneyCost.IdentifySyringe;

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
				.WithUnlock(new TU_DesignerUnlock());
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
								agent.SayDialogue(VDialogue.Scientist_Bought2);
								interactingAgent.statusEffects.IdentifySyringe(item.contents[0]);
							}
						}
						else
						{
							interactingAgent.SayDialogue(VDialogue.NA_AlreadyIdentified);
							GC.audioHandler.Play(interactingAgent, VanillaAudio.CantDo);
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