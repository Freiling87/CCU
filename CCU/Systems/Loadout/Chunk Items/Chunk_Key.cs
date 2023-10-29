using BunnyLibs;
using CCU.Traits.Loadout;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Loadout_Chunk_Items
{
	public class Chunk_Key : T_Loadout, IRefreshAtEndOfLevelStart
	{
		public bool BypassUnlockChecks => false;
		public void Refresh() { }
		public void Refresh(Agent agent)
		{
			List<Door> validDoors = GC.objectRealList
				.Where(or => or is Door door && door.startingChunk == agent.startingChunk && door.locked && door.distributedKey is null && !door.hasDetonator && door.extraVar != 10)
				.Cast<Door>().ToList();
			// extraVar == 10 : "PanicRoom" I think

			if (validDoors.Count > 0)
			{
				foreach (Door door in validDoors)
					door.distributedKey = agent;

				InvItem key = agent.inventory.AddItem(VItemName.Key, 1);
				key.specificChunk = agent.startingChunk;
				key.specificSector = agent.startingSector;
				key.chunks.Add(agent.startingChunk);
				key.sectors.Add(agent.startingChunk);
				key.contents.Add(
					agent.startingChunkRealDescription == VChunkType.Generic
						? VChunkType.GuardPostSeeWarning
						: agent.startingChunkRealDescription);
				agent.oma.hasKey = true;
			}
		}
		public bool RunThisLevel() => true;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Chunk_Key>()
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
				.WithUnlock(new TU_DesignerUnlock
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