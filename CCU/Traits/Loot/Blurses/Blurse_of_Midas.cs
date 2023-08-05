using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
    internal class Blurse_of_Midas : T_LootDrop
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Blurse_of_Midas>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop their Money. It can still be acquired through other means (Mugging, pickpocketing).",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_Midas)),
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

		public override bool IsUnspillable(InvItem invItem) =>
			invItem.invItemName == vItem.Money;
    }
}
