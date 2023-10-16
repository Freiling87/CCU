using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
	public class Brainless : T_Behavior
	{
		public override bool LosCheck => false;
		public override string[] GrabItemCategories => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Brainless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent won't react to anything."),
					[LanguageCode.Spanish] = "Este NPC no reacciona a nada, pero le gusta ver youtubers de drama de ves en cuando",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Brainless)),
					[LanguageCode.Spanish] = "Descerebrado",
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