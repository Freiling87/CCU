using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
    internal class Bankrupt_75 : T_Loadout
    {
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Bankrupt_75>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "75% chance to spawn without Money.",
                    [LanguageCode.Spanish] = "chance de 75% de spawnear sin dinero.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bankrupt_75), "Bankrupt 75%"),
                    [LanguageCode.Spanish] = "Bancarrota 75%",
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