using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Number_to_Pain : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.ResistDamageMedium;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Number_to_Pain>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains a 33% damage resistance upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC tiene una resistencia de 33% contra todo daño al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Number_to_Pain)),
					[LanguageCode.Spanish] = "Entumecido al Dolor",

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
		
		
	}
}
