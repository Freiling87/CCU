using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	/// <summary>
	/// Named distinct from the Status effect
	/// </summary>
	public class Dizzy_EOD : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Dizzy;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Dizzy_EOD>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character makes anyone nearby dizzy. I can't think of any possible reason for this."),
					[LanguageCode.Spanish] = "Al morir, este NPC explota como una granada aturdidora, Hay una razon para esto pero uhhhhhh, digamos que me olvide.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Dizzy_EOD), "Dizzy"),
					[LanguageCode.Spanish] = "Aturdidor",

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