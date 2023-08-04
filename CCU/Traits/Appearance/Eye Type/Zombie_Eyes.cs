﻿using RogueLibsCore;

namespace CCU.Traits.App_ET1
{
	public class Zombie_Eyes : T_EyeType
	{
		public override string[] Rolls => new string[] { "EyesZombie" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Zombie_Eyes>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
                    [LanguageCode.Spanish] = "Agrega este par de ojos a los que el personaje puede usar.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Zombie_Eyes)),
                    [LanguageCode.Spanish] = "Ojos de Zombie",
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
