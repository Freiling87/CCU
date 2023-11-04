using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Gambler : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.FeelingLucky;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gambler>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("You feel lucky, punk? I do."),
					[LanguageCode.Spanish] = "Que suerte tener a alguein que use este mod en español, ah si el NPC tiene suerte, esto afecta ataques criticos y ciertos rasgos.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gambler)),
					[LanguageCode.Spanish] = "Apostador",

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
