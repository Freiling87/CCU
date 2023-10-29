using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Play_Bad_Music : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => null;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null; // Determined in code

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Play_Bad_Music>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be paid to play a bad song, clearing the chunk out. They can also play Mayor Evidence on Turntables."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Play_Bad_Music)),

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
