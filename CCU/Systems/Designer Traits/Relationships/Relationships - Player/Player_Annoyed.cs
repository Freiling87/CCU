﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Annoyed : T_Rel_Player
	{
		public override string Relationship => VRelationship.Annoyed;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Annoyed>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Annoyed at players.",
					[LanguageCode.Spanish] = "Este NPC es Iritado al jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Annoyed)),
					[LanguageCode.Spanish] = "Iritado al Jugador",

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
