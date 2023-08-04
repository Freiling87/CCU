﻿using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
    internal class FunnyPack : T_Loadout
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<FunnyPack>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent can spawn with an additional Pocket item.",
                    [LanguageCode.Spanish] = "NPC puede spawnear con un item de bolsillo addicional.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(FunnyPack)),
                    [LanguageCode.Spanish] = "Riñonero",
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