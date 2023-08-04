using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
    internal class Desperate : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(6, 11);

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Desperate>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $6 to $11.",
                    [LanguageCode.Spanish] = "NPC spawnea con $6 hasta $11.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Desperate)),
                    [LanguageCode.Spanish] = "Desperado",
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