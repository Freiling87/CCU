using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
	public class Player_Hostile : T_Rel_Player
	{
		public override string Relationship => VRelationship.Hostile;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Player_Hostile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character is Hostile to players.",
					[LanguageCode.Spanish] = "Este NPC es Hostil al jugador.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Player_Hostile)),
					[LanguageCode.Spanish] = "Hostil al Jugador",

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
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
