using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Invisibilist : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Invisible;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Invisibilist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will become Invisible upon entering combat. And yes, it's spelled right."),
					[LanguageCode.Spanish] = "Este NPC se vuelve invisible al entrar en combate, olvidate de las miradas inoportunas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Invisibilist)),
					[LanguageCode.Spanish] = "Invisiblelista",

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
