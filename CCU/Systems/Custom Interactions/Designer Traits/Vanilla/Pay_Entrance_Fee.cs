﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Pay_Entrance_Fee : T_Interaction
	{
		public override bool AllowUntrusted => true;
		public override string ButtonID => VButtonText.PayEntranceFee;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => VDetermineMoneyCost.Bribe;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pay_Entrance_Fee>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if paid with cash.\n\nBypasses Untrusting traits."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pay_Entrance_Fee)),

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
		
		
	}
}
