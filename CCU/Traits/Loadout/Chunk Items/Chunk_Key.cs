using CCU.Traits.Loadout;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Loadout_Chunk_Items
{
    public class Chunk_Key : T_Loadout, IRefreshAtLevelStart
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Key>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Key Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Key)),
					
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

		public void RefreshAtLevelStart(Agent agent)
		{
			List<Door> validDoors = GC.objectRealList
				.Where(or => or is Door door && door.startingChunk == agent.startingChunk && door.locked && door.distributedKey is null && !door.hasDetonator && door.extraVar != 10)
				.Cast<Door>().ToList();
			// extraVar == 10 : "PanicRoom" I think

			if (validDoors.Count > 0)
			{
				foreach (Door door in validDoors)
					door.distributedKey = agent;

				InvItem key = agent.inventory.AddItem(vItem.Key, 1);
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
	}
}