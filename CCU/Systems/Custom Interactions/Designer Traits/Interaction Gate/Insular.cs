using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
	public class Insular : T_InteractionGate
	{
		public override int MinimumRelationship => 3;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Insular>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will not interact with anyone who has a relationship with their faction at Neutral or worse."),
					[LanguageCode.Spanish] = "Este NPC no interactuara con quien sea neutral o peor.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Insular)),
					[LanguageCode.Spanish] = "Insular",

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
