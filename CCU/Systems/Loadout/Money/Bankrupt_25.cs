using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Bankrupt_25 : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bankrupt_25>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "25% chance to spawn without Money.",
					[LanguageCode.Spanish] = "chance de 25% de spawnear sin dinero.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bankrupt_25), "Bankrupt 25%"),
					[LanguageCode.Spanish] = "Bancarrota 25%",
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