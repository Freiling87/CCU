using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
    internal class Blurse_of_Valhalla : T_LootDrop
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Blurse_of_Valhalla>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop weapons or armor.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_Valhalla)),
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
			LoadoutTools.GetSlotFromItem(invItem) != LoadoutTools.Slots.Pockets;
    }
}
