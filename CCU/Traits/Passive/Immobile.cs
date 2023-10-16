using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Immobile : T_CCU
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Immobile>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent can't move. They can still be knocked back, so add Immovable if you don't want them to."),
					[LanguageCode.Spanish] = "Este personaje no puede moverse, aun asi puedes empujarlo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Immobile)),
					[LanguageCode.Spanish] = "Inmobil",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
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