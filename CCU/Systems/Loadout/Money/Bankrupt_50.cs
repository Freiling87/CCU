using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Bankrupt_50 : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bankrupt_50>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "50% chance to spawn without Money.",
					[LanguageCode.Spanish] = "chance de 50% de spawnear sin dinero.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bankrupt_50), "Bankrupt 50%"),
					[LanguageCode.Spanish] = "Bancarrota 50%",
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