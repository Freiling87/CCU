using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Ridiculous : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Ridiculous;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Ridiculous>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character explodes. Over 125 Slaves' worth, a fantastic value!\n\n" +
					"This is the explosion from the Hidden Bombs disaster. It means instant death for the player."),
					[LanguageCode.Spanish] = "Al morir, este NPC explota al equivalente de una bola de esclavos del tamaño del sol, increible!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ridiculous)),
					[LanguageCode.Spanish] = "Ridiculo",

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