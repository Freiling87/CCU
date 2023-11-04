using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Submissive : T_Rel_Player
	{
		public override string Relationship => VRelationship.Submissive;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Submissive>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Submissive to Players.",
					[LanguageCode.Spanish] = "Este NPC es Sumiso al jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Submissive)),
					[LanguageCode.Spanish] = "Sumiso al Jugador",

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
