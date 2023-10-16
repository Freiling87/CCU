﻿using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Use_Blood_Bag : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.UseBloodBag;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Use_Blood_Bag>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can help the player use a Blood Bag in their inventory."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Use_Blood_Bag)),

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
