﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Aligned : T_Rel_Player
	{
		public override string Relationship => VRelationship.Aligned;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Aligned>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Aligned to players.",
					[LanguageCode.Spanish] = "Este NPC es Aliado al jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Aligned)),
					[LanguageCode.Spanish] = "Aliado al Jugador",

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
