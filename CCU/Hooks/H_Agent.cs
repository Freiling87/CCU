using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Hooks
{
	public class H_Agent : HookBase<PlayfieldObject>
	{
		// Instance = host Agent

		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		protected override void Initialize()
		{
			//Core.LogMethodCall();
			GrabAppearance();
			mustRollAppearance = true;
			SceneSetterFinished = false; // Avoids removal from series mid-traversal
		}

		public void GrabAppearance()
		{
			//Core.LogMethodCall();
			Agent agent = (Agent)Instance;
			//logger.LogDebug("Agent: " + agent.agentRealName);
			SaveCharacterData save = agent.customCharacterData;
			bodyColor = save.bodyColorName;
			bodyType = save.bodyType;
			eyesType = save.eyesType;
			skinColor = save.skinColorName;
		}

		public void Reset()
		{
			ClassifierScannedAgents.Clear();
		}

		public bool SceneSetterFinished;

		public bool WalkieTalkieUsed;
		public bool HiredPermanently;
		public int SuicideVestTimer;

		public bool mustRollAppearance;
		public string bodyColor;
		public string bodyType;
		public string eyesType;
		public string skinColor;

		public List<string> ClassifierScannedAgents = new List<string> { };
	}
}
