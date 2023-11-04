using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Fireproofer : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.ResistFire;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fireproofer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains a layer of fire resistance when entering combat."),
					[LanguageCode.Spanish] = "Este NPC se da un pequeño spray de anti-fuego al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fireproofer)),
					[LanguageCode.Spanish] = "Apruebandose de Fuego",

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
