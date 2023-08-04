using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
    internal class Rich : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(41, 61);

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Rich>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $41 to $61.",
                    [LanguageCode.Spanish] = "NPC spawnea con $41 hasta $61.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Rich)),
                    [LanguageCode.Spanish] = "Rico",
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