using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class The_Last_Whiff : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Nicotine;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<The_Last_Whiff>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character smokes a cigarette right when they get into a fight. How fuckin' cool are they??"),
					[LanguageCode.Spanish] = "Este NPC se fuma uno antes del combate, demostrando lo genial que es el cancer de pulmon en el cine.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(The_Last_Whiff)),
					[LanguageCode.Spanish] = "Infumable",

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
