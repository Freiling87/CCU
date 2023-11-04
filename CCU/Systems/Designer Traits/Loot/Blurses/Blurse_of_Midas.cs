using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
	public class Blurse_of_Midas : T_LootDrop
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Blurse_of_Midas>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop their Money. It can still be acquired through other means (Mugging, pickpocketing).",
					[LanguageCode.Spanish] = "Al ser neutralizado, este NPC nunca soltara dinero, pero aun puede perderlo a traves de otros metodos (Atracando, cartereando).",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_Midas)),
					[LanguageCode.Spanish] = "Maldición de Midas",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		

		public override bool IsUnspillable(InvItem invItem) =>
			invItem.invItemName == VItemName.Money;
	}
}
