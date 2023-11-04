﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Pay_Debt : T_Interaction
	{
		public override bool AllowUntrusted => true;
		public override string ButtonID => VButtonText.PayDebt;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pay_Debt>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can accept debt payments.\n\n" +
					"Note: If you want them to lend money as well, use {0} too.\n\nBypasses Untrusting traits.", DocumentationName(typeof(Borrow_Money))),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pay_Debt)),

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
