using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Berserker : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Rage;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Berserker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character just totally sees red when he gets mad, bro. Like not even kidding bro, he loses control."),
					[LanguageCode.Spanish] = "Este ¨NPC cuando se enoja, es la ultima gota, se vuelve completamente loco.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Berserker)),
					[LanguageCode.Spanish] = "Berserker",

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
