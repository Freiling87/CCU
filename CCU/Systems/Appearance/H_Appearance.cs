using CCU.Traits.App;
using HarmonyLib;
using RogueLibsCore;

namespace CCU
{
	public class H_Appearance : HookBase<PlayfieldObject>
	{
		// NOTE: Instance = host Agent

		public bool mustRollAppearance;
		public string bodyColor;
		public string bodyType;
		public string eyesType;
		public string skinColor;

		//	Called by H_Agent
		protected override void Initialize()
		{
			GrabAppearance();
		}

		public void Reset() { }

		public void GrabAppearance()
		{
			Agent agent = (Agent)Instance;

			mustRollAppearance =
				agent.HasTrait<Dynamic_Player_Appearance>()
				|| agent.isPlayer == 0;

			SaveCharacterData save = agent.customCharacterData;
			bodyColor = save.bodyColorName;
			bodyType = save.bodyType;
			eyesType = save.eyesType;
			skinColor = save.skinColorName;
		}
	}

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent_AppearanceHook
	{
		[HarmonyPrefix, HarmonyPatch("Start")]
		public static bool Start_CreateHook(Agent __instance)
		{
			__instance.GetOrAddHook<H_Appearance>().Reset();
			return true;
		}
	}
}