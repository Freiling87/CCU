using CCU.Traits.Interaction;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU
{
	public class H_AgentInteractions : HookBase<PlayfieldObject>
	{
		public bool HiredPermanently;
		public InteractionState interactionState;
		public List<string> languages = new List<string> { };

		//	H_AgentSomething
		public bool SceneSetterFinished;

		//	H_AgentInventory
		public List<string> classawareStoredAgents = new List<string> { };
		public int SuicideVestTimer;
		public bool WalkieTalkieUsed;

		protected override void Initialize()
		{
			interactionState = InteractionState.Default;
		}
		public void Reset()
		{
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
}