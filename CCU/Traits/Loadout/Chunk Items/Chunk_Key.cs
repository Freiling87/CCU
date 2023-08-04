﻿using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Chunk_Items
{
    public class Chunk_Key : T_Loadout
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Key>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Key Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
                    [LanguageCode.Spanish] = "Este NPC se le asignara la llave del edificio del cual sea dueño, En caso de multiples NPCs con este rasgo, se eligira uno aleatoriamente. Normalmete la llave se asigna a los Empleados.",

                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Key)),
                    [LanguageCode.Spanish] = "Llave del Edificio",

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
