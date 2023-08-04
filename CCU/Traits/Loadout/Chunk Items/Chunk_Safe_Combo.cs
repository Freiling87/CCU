using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Chunk_Items
{
    public class Chunk_Safe_Combo : T_Loadout
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Safe_Combo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Safe Combo Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
                    [LanguageCode.Spanish] = "Este NPC se le asignara la Combinacion de la Caja Fuerte del edificio del cual sea dueño, En caso de multiples NPCs con este rasgo, se eligira uno aleatoriamente. Normalmete la Combinacion se asigna a los Empleados.",

                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Safe_Combo)),
                    [LanguageCode.Spanish] = "Combinacion de la Caja Fuerte",

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
