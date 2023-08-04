using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Chunk_Items
{
    public class Chunk_Mayor_Badge : T_Loadout
	{
        //[RLSetup]
        public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Mayor_Badge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Badge Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
                    [LanguageCode.Spanish] = "Este NPC se le asignara la Placa de Visitante del Alcalde, En caso de multiples NPCs con este rasgo, se eligira uno aleatoriamente. Normalmete la placa se asigna a el Empleado Central.",

                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Mayor_Badge)),
                    [LanguageCode.Spanish] = "Placa de Visitante",

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
