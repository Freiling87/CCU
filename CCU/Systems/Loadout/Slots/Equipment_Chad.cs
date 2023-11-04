using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	public class Equipment_Chad : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Equipment_Chad>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent will never fail to generate an equippable item in a slot, if there are any added to their item pool. Including the Stacy slot! REEEEEEEEEEEEE",
					[LanguageCode.Spanish] = "Este NPC siempre generara un item equipable en un espacio si tiene uno.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Equipment_Chad)),
					[LanguageCode.Spanish] = "Equipaddicto",
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
