using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
    internal class Blurse_of_Softlock : T_LootDrop
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Blurse_of_Softlock>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop Keys, Safe Combos, and other crucial items. This can soft-lock a game from progressing. The items can still be acquired through nonviolent means.\n\n" +
					"<i>Tales tell of the pirate Softlock, spoken only in whispers. Sort of a mean guy! And they say that the men who finally ran him down wandered the seas until their doom, in search of a Signed Baseball they never found. Yarrrr.</i>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_Softlock)),
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
			invItem.questItem ||
			T_LootDrop.SoftLockItems.Contains(invItem.invItemName);
    }
}
