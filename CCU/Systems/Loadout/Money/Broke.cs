using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Broke : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(1, 6);

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Broke>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $1 to $6.",
					[LanguageCode.Spanish] = "NPC spawnea con $1 hasta $6.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Broke)),
					[LanguageCode.Spanish] = "Seco",
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