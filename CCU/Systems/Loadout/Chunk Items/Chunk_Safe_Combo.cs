using BunnyLibs;
using CCU.Traits.Loadout;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Loadout_Chunk_Items
{
	public class Chunk_Safe_Combo : T_Loadout, IRefreshAtEndOfLevelStart
	{
		public bool BypassUnlockChecks => false;
		public void Refresh() { }
		public void Refresh(Agent agent)
		{
			List<Safe> validSafes = GC.objectRealList
				.Where(or => or is Safe safe && safe.startingChunk == agent.startingChunk && safe.locked && safe.distributedKey is null)
				.Cast<Safe>().ToList();

			if (validSafes.Count > 0)
			{
				foreach (Safe safe in validSafes)
					safe.distributedKey = agent;

				InvItem safeCombo = agent.inventory.AddItem(VItemName.SafeCombination, 1);
				safeCombo.specificChunk = agent.startingChunk;
				safeCombo.specificSector = agent.startingSector;
				safeCombo.chunks.Add(agent.startingChunk);
				safeCombo.sectors.Add(agent.startingChunk);
				safeCombo.contents.Add(agent.startingChunkRealDescription);
				agent.oma.hasKey = true;
			}
		}
		public bool RunThisLevel() => true;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Chunk_Safe_Combo>()
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
