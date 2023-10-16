using RogueLibsCore;
using System;

namespace CCU.Traits.CombatGeneric
{
	public class Melee_Competent : T_MeleeSkill
	{
		public override int MeleeSkill => 1;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Competent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Average attack frequency, like Cannibal, Cop & Firefighter."),
					[LanguageCode.Spanish] = "Frequencia de ataque standard, como Canibal, Policía o Bombero.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Melee_Competent)),
					[LanguageCode.Spanish] = "Facil al Garrote",

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
	}
}
