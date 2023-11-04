using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Neutral : T_Rel_Player
	{
		public override string Relationship => VRelationship.Neutral;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Neutral>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Neutral to players.",
					[LanguageCode.Spanish] = "Este NPC es Neutral al jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Neutral)),
					[LanguageCode.Spanish] = "Neutral al Jugador",

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
