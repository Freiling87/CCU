using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Specistist : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Specistist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent is a valid target for Specists. I think they must be prejudiced against them or something."),
					[LanguageCode.Spanish] = "Este personaje es conciderado rival de personajes con el rasgo Aborrecedor del Gorila. El racismo nunca esta bien!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Specistist)),
					[LanguageCode.Spanish] = "Aborrecedor del Aborrecedor del Gorila",
				})
				.WithUnlock(new TraitUnlock_CCU
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