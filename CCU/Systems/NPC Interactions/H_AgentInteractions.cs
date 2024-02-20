using CCU.Interactions;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CCU
{
	public class H_AgentInteractions : HookBase<PlayfieldObject>
	{
		public bool HiredPermanently; 
		private InteractionState _interactionState;
		public InteractionState interactionState
		{
			get
			{
				return _interactionState;
			}
			set
			{
				if (value != _interactionState)
					LogCallingMethod("Setter", $"{_interactionState, 20} --> {value, 16}");

				_interactionState = value;
			}
		}
		private void LogCallingMethod(string type, string message)
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame frame = stackTrace.GetFrame(2);
			MethodBase method = frame.GetMethod();
			BLLogger.GetLogger().LogDebug($"{type.ToUpper()}: '{method.DeclaringType}.{method.Name}': [{message}]");
		}


		//	H_AgentSomething
		public bool SceneSetterFinished;

		//	H_AgentInventory
		public List<string> classawareStoredAgents = new List<string> { };
		public int SuicideVestTimer;
		public bool WalkieTalkieUsed;

		protected override void Initialize()
		{
			Reset();
		}
		public void Reset()
		{
			interactionState = InteractionState.Default;
			classawareStoredAgents.Clear();
		}
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent_AgentInteractionsHook
	{
		[HarmonyPrefix, HarmonyPatch("Start")]
		public static bool Start_CreateHook(Agent __instance)
		{
			__instance.GetOrAddHook<H_AgentInteractions>().Reset();
			return true;
		}
	}

	[HarmonyPatch(typeof(PlayfieldObject))]
	public class P_PlayfieldObject_AgentInteractionsHook
	{
		[HarmonyPostfix, HarmonyPatch(nameof(PlayfieldObject.StopInteraction), new[] { typeof(bool) })]
		public static void ResetState(PlayfieldObject __instance)
		{
			if (__instance is Agent agent)
			{
				agent.GetHook<H_AgentInteractions>().interactionState = InteractionState.Default;
			}
		}
	}

	[HarmonyPatch(typeof(InteractionModel))]
	public class P_InteractionModel_AgentInteractionsHook
	{
		[HarmonyPrefix, HarmonyPatch("OnPressedButton2")]
		public static bool PressedDoneButton(InteractionModel __instance, string buttonName)
		{
			PlayfieldObject interactedObject = __instance.Instance;
			Agent playerAgent = __instance.Agent;

			BLLogger.GetLogger().LogDebug($"Go Up Menu Tier: '{playerAgent.agentRealName,16} / {buttonName,16} / {playerAgent.GetHook<H_AgentInteractions>().interactionState,20}");

			if (buttonName == VButtonText.Done 
				&& interactedObject.GetHook<H_AgentInteractions>().interactionState != InteractionState.Default)
			{ 
				BLLogger.GetLogger().LogDebug("		Done Button");
				// TODO: Track previous state, in order to allow more than one tier deep
				interactedObject.GetHook<H_AgentInteractions>().interactionState = InteractionState.Default;
				interactedObject.RefreshButtons();
				return false;
			}

			return true;
		}
	}
}