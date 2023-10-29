using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Leave_Weapons_Behind : T_Interaction
	{
		public override bool AllowUntrusted => true;
		public override string ButtonID => VButtonText.LeaveWeaponsBehind;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		// Should include FollowersLeaveWeaponsBehind

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Leave_Weapons_Behind>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be interacted with to drop all weapons in the Player's inventory.\n\nBypasses Untrusting traits."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Leave_Weapons_Behind)),

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
