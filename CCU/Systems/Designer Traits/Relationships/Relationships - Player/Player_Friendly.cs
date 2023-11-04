using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Friendly : T_Rel_Player
	{
		public override string Relationship => VRelationship.Friendly;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Friendly>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Friendly to Players.",
					[LanguageCode.Spanish] = "Este NPC es Amistoso al Jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Friendly)),
					[LanguageCode.Spanish] = "Amistoso al Jugador",

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
