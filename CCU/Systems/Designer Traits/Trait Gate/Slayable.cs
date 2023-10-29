using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Slayable : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Slayable>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This NPC is hostile and provides an XP bonus to Scientist Slayers."),
					[LanguageCode.Spanish] = "Este personaje es conciderado rival de personajes con el rasgo Aborrecedor del Cientifico.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slayable)),
					[LanguageCode.Spanish] = "Aborrecedor del Aborrecedor del Cientifico",

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
