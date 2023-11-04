using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Cool_Cannibal : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cool_Cannibal>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This NPC's behaviors will react to the player's Cool with Cannibals trait."),
					[LanguageCode.Spanish] = "Este NPC cambiara su comportamiento si el jugador tiene el rasgo Amigo de los Canibales, volviendose neutral.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cool_Cannibal)),
					[LanguageCode.Spanish] = "Amigo Canibalisoso",

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
