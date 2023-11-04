using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Oil_Spill : T_ExplodeOnDeath
	{
		public override string ExplosionType => "";// CExplosionType.OilSpill;
												   // Category iced for now

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Oil_Spill>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character splashes oil everywhere. Don't worry, it's inflammable."),
					[LanguageCode.Spanish] = "Al morir, este NPC explota en un charco de petroleo combustible. Menos mal que no fuman.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Oil_Spill)),
					[LanguageCode.Spanish] = "Petroleo",

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