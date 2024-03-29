﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Secret_Hate : T_Rel_Player
	{
		public override string Relationship => null;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Secret_Hate>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character will ambush Players.",
					[LanguageCode.Spanish] = "Este NPC se volvera hostil caundo el jugador se acerque.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Secret_Hate)),
					[LanguageCode.Spanish] = "Secretamente Hostil al Jugador",

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
