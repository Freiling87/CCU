using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Numb_to_Pain : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.ResistDamageSmall;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Numb_to_Pain>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains a 20% damage resistance upon entering combat."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Numb_to_Pain)),
					[LanguageCode.Spanish] = "Poco Entumecidicimo al Dolor",

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
