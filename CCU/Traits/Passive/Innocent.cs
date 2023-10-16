using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Innocent : T_CCU, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Innocent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will never be designated Guilty."),
					[LanguageCode.Spanish] = "Este NPC no puede volverse nunca culpable.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Innocent)),
					[LanguageCode.Spanish] = "Inocente",

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public void SetupAgentStats(Agent agent)
		{
			agent.oma.mustBeGuilty = false;
			agent.oma.mustBeInnocent = true;
			agent.oma._mustBeInnocent = true;
		}
	}
}