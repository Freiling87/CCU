using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Honorable_Thief : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Honorable_Thief>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This Agent's behaviors will react to the player's Honor Among Thieves trait.\n\n" +
					"<color=green>Interactions</color>\n" +
					"- Will not target the player with Pickpocketing if they have Honor Among Thieves.\n" +
					"- Will not sell items unless player has Honor Among Thieves."),
					[LanguageCode.English] = String.Format("El comportamiento de este NPC cambiara si el jugador tiene el rasgo Honor entre Ladrones.\n\n" +
					"<color=green>Interaciones</color>\n" +
					"- No robara del jugador si tienen Honor entre Ladrones.\n" +
					"- No le vendera al jugador almenos que tenga Honor entre Ladrones."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Honorable_Thief)),
					[LanguageCode.Spanish] = "Ladron Honorable",

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
