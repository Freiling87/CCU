using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Cop_Access : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cop_Access>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Will not sell to the player if they don't have The Law."),
					[LanguageCode.Spanish] = "Este NPC no le vendera al jugador al menos que tengan el rasgo La Ley",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_Access)),
					[LanguageCode.Spanish] = "Acceso Policial",

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
