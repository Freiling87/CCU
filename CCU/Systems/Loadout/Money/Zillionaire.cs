﻿using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Zillionaire : T_PocketMoney
	{
		public override int MoneyAmount => 1000;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Zillionaire>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $1000.",
					[LanguageCode.Spanish] = "NPC spawnea con $1000.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Zillionaire)),
					[LanguageCode.Spanish] = "Zillonario",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}