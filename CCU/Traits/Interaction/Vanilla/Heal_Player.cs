using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Heal_Player : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.Heal;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => VDetermineMoneyCost.Heal;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Heal_Player>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can heal, for money.\n\nThey may or may not keep it real."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Heal_Player)),

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
