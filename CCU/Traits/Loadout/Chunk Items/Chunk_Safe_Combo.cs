using CCU.Traits.Loadout;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Loadout_Chunk_Items
{
    public class Chunk_Safe_Combo : T_Loadout, IRefreshAtLevelStart
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Safe_Combo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Safe Combo Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Safe_Combo)),
					
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
			List<Safe> validSafes = GC.objectRealList
				.Where(or => or is Safe safe && safe.startingChunk == agent.startingChunk && safe.locked && safe.distributedKey is null)
				.Cast<Safe>().ToList();

			if (validSafes.Count > 0)
			{
				foreach (Safe safe in validSafes)
					safe.distributedKey = agent;

				InvItem safeCombo = agent.inventory.AddItem(vItem.SafeCombination, 1);
				safeCombo.specificChunk = agent.startingChunk;
				safeCombo.specificSector = agent.startingSector;
				safeCombo.chunks.Add(agent.startingChunk);
				safeCombo.sectors.Add(agent.startingChunk);
				safeCombo.contents.Add(agent.startingChunkRealDescription);
				agent.oma.hasKey = true;
			}
		}
	}
}
