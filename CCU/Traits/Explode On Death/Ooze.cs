using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Ooze : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Ooze;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ooze>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character releases The Ooze. They say that's the first thing that happens."),
					[LanguageCode.Spanish] = "Al morir, este NPC suelta EL FLUIDOOOO, que miedo, y que asco",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ooze)),
					[LanguageCode.Spanish] = "FLuido",

				})
				.WithUnlock(new TraitUnlock_CCU
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