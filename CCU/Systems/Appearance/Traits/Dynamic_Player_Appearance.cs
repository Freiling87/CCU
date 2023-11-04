using BunnyLibs;

using RogueLibsCore;

namespace CCU.Traits.App
{
	public class Dynamic_Player_Appearance : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Dynamic_Player_Appearance>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Activates the Appearance System for player versions of this character.",
					[LanguageCode.Spanish] = "Activa el sistema cosmético de rasgos para la versión jugable del personaje.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Dynamic_Player_Appearance)),
					[LanguageCode.Spanish] = "Apariencia Dinámica",

				})
				.WithUnlock(new TU_DesignerUnlock { });
		}
		
		

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			if (!agent.clonedAgent)
				agent.GetOrAddHook<H_Appearance>().mustRollAppearance = true;
		}
	}
}