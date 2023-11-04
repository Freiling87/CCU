using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Sure_I_Can : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.KillerThrower;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sure_I_Can>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character has a can-do attitude about everything, including whether they can kill you with one hit of a thrown item for a limited time upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC gana un brazo derecho muy bueno al entrar en combate, por lo que puede matarte con una sola piedra.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sure_I_Can), "Sure I Can!"),
					[LanguageCode.Spanish] = "Mastirador",

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
