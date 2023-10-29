using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Z_Infected : T_CCU, ISetupAgentStats
	{
		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.zombieWhenDead = true;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Z_Infected>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is infected with the Z-Virus. They are not a zombie yet, but will become one when killed."),
					[LanguageCode.Spanish] = "Este NPC esta infectado por el Virus Z, asi que al morir se pintan de verde y se ponen a matar gente, lo usual.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Z_Infected), ("Z-Infected")),
					[LanguageCode.Spanish] = "Infectado Z",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { DesignerName(typeof(Possessed)) },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
	}
}