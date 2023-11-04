using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Firebomb : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.FireBomb;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Firebomb>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character splashes burning oil everywhere. What did they eat?!"),
					[LanguageCode.Spanish] = "Al morir, Este NPC explota en llamas, esto es una condicion real la cual te podria suceder con 200 casos este siglo y contando.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Firebomb)),
					[LanguageCode.Spanish] = "Fuegooo",

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