using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Numbest_to_Pain : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.ResistDamageLarge;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Numbest_to_Pain>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains a 50% damage resistance upon entering combat."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Numbest_to_Pain)),
					[LanguageCode.Spanish] = "Entumecidicimo al Dolor",

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
