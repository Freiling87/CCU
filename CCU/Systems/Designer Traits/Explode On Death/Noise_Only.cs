using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Noise_Only : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.NoiseOnly;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Noise_Only>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character makes a REALLY loud death rattle."),
					[LanguageCode.Spanish] = "Al morir, este NPC se desintegra de manera muy ruidosa.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Noise_Only)),
					[LanguageCode.Spanish] = "Solo Ruidos",

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