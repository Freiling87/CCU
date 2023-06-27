using BepInEx.Logging;
using CCU.Traits.App;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Hooks
{
	public class H_Agent : HookBase<PlayfieldObject>
	{
		// NOTE: Instance = host Agent

		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public bool SceneSetterFinished;

		public bool WalkieTalkieUsed;
		public bool HiredPermanently;
		public int SuicideVestTimer;

		public bool mustRollAppearance;
		public string bodyColor;
		public string bodyType;
		public string eyesType;
		public string skinColor;

		public List<string> classawareStoredAgents = new List<string> { };

		public List<string> languages = new List<string> { };

		public int originalOwnerID;

		protected override void Initialize()
		{
			Agent agent = (Agent)Instance;

			mustRollAppearance = 
				agent.HasTrait<Dynamic_Player_Appearance>()
				|| agent.isPlayer == 0;

			GrabAppearance();
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
			classawareStoredAgents.Clear();
		}
	}
}