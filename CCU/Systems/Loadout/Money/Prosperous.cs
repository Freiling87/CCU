using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Prosperous : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(26, 41);

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Prosperous>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $26 to $41.",
					[LanguageCode.Spanish] = "NPC spawnea con $26 hasta $41.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Prosperous)),
					[LanguageCode.Spanish] = "Prospero",
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