using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Water : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Water;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Water>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character splashes water everywhere. Contributing factor to death: Uromisotisis poisoning. Don't hold it in, folks."),
					[LanguageCode.Spanish] = "Al morir, este NPC sufre una nautica y explota como un globo de agua, ni se te ocurra tomar eso.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Water)),
					[LanguageCode.Spanish] = "Agua",

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