using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Drug_Warrior;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Combat_
{
	public class Nth_Wind : T_Combat
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Nth_Wind>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Refreshes certain flags after combat ends, e.g. Drug Warrior, allowing them to be used across multiple combats."),
					[LanguageCode.Spanish] = "Refresca ciertas abilidades al terminar el combate, como effectos de Drogladiador Permitiendo que se usen en multiples combates.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Nth_Wind)),
					[LanguageCode.Spanish] = "Quintacillima Dosis",

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }

		public static void ResetFlags(Agent agent)
		{
			if (agent.HasTrait<Nth_Wind>())
			{
				if (agent.GetTraits<T_DrugWarrior>().Any())
				{
					agent.combat.canTakeDrugs = true;
					agent.oma.combatTookDrugs = false;
				}

				if (agent.HasTrait<Backed_Up>())
					agent.GetOrAddHook<H_AgentInteractions>().WalkieTalkieUsed = false;
			}
		}
	}
}
