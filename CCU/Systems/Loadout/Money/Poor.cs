using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Poor : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(11, 26);

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Poor>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $11 to $26.",
					[LanguageCode.Spanish] = "NPC spawnea con $11 hasta $26.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Poor)),
					[LanguageCode.Spanish] = "Pobre",
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