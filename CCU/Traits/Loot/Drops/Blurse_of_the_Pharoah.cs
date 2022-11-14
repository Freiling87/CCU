using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
    internal class Blurse_of_the_Pharoah : T_LootDrop
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Blurse_of_the_Pharoah>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop their inventory items. Important items like keys and quest items are still dropped.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_the_Pharoah)),
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

		public override bool ProtectedItem(InvItem invItem) =>
			LoadoutTools.GetSlotFromItem(invItem) == LoadoutTools.Slots.Pockets &&
			!SoftLockItems.Contains(invItem.invItemName);
    }
}
