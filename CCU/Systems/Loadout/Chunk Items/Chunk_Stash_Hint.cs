using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Chunk_Items
{
	public class Chunk_Stash_Hint : T_Loadout
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Chunk_Stash_Hint>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Stash Hint Holder. If multiple characters have this trait, one will be chosen randomly.",
					[LanguageCode.Spanish] = "Este NPC se le asignara la (quotes)Pista(quotes) del edificio del cual sea dueño, En caso de multiples NPCs con este rasgo, se eligira uno aleatoriamente. Que significa esto? lo revisare despues porque ni el desarollador lo sabe.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Stash_Hint)),
					[LanguageCode.Spanish] = "Pista del Edificio",
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
