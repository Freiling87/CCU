using RogueLibsCore;

namespace CCU.Traits.Loadout_Money
{
	public class Wealthy : T_PocketMoney
	{
		public override int MoneyAmount => UnityEngine.Random.Range(80, 101);

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Wealthy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with $80 to $101.",
					[LanguageCode.Spanish] = "NPC spawnea con $80 hasta $101.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wealthy)),
					[LanguageCode.Spanish] = "Adinerado",
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