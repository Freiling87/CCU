using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Electrocutioner : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.ElectroTouch;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Electrocutioner>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will gain Electro-Touch upon entering combat, and then say some hokey pun about how they're going to show the hero something shocking. Yawn."),
					[LanguageCode.Spanish] = "Este NPC se le da un toque Electrico al entrar en combate, pone los pelos de punta! no?",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Electrocutioner)),
					[LanguageCode.Spanish] = "Electrocucionistarista",

				})
				.WithUnlock(new TU_DesignerUnlock
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
