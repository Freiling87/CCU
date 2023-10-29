using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Flasher : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Fast;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Flasher>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will become Fast upon entering combat... like The Flash. They absolutely do *not* reveal themselves to people, and we are working on getting the name changed."),
					[LanguageCode.Spanish] = "Este NPC es muy rapido en todo! no pregunten de su club de fans, nunca pregunten sobre su pasado.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Flasher)),
					[LanguageCode.Spanish] = "Flashista",

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
