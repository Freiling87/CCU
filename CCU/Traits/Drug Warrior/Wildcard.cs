using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Wildcard : T_DrugWarrior
	{
		public override string DrugEffect => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Wildcard>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will use a random syringe upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC usa una jeringa aleatoria al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wildcard)),
					[LanguageCode.Spanish] = "Liquidos Locos",

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
