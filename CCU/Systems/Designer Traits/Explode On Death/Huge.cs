using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Huge : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Huge;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Huge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character explodes. 32 Slaves' worth! AN INCREDIBLE VALUE!"),
					[LanguageCode.Spanish] = "Al morir, este NPC explota como 32 esclavos haciendo... algo, pero para ti eso es justo PERFECTO, vamos hombre atomico!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Huge)),
					[LanguageCode.Spanish] = "Enorme",

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