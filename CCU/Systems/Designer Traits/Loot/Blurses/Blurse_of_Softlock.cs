using RogueLibsCore;

namespace CCU.Traits.Loot_Drops
{
	public class Blurse_of_Softlock : T_LootDrop
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Blurse_of_Softlock>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When agent is neutralized, they do not drop Keys, Safe Combos, and other crucial items. This can soft-lock a game from progressing. The items can still be acquired through nonviolent means.\n\n" +
					"<i>Tales tell of the pirate Softlock, spoken only in whispers. Sort of a mean guy! And they say that the men who finally ran him down wandered the seas until their doom, in search of a Signed Baseball they never found. Yarrrr.</i>",
					[LanguageCode.Spanish] = "Al ser neutralizado, este NPC no soltara ningun item crucial (Llaves, Items de Mision, Combinaciones), Esto puede causar un softlock en el juego removiendo la abilidad de progresar. Los items aun se pueden conseguir por los metodos no-violentos.\n\n" +
					 "<i>Terrorificas historias piratas hablan sobre el tenebrozo Capitan Soflothk, dichas por marineros perdidos buscando tesoro que nunca se encontro, es un secreto donde se lo metio. Yarrrrr!!!.</i>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Blurse_of_Softlock)),
					[LanguageCode.Spanish] = "Maldición de Softlock",
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
			invItem.questItem ||
			T_LootDrop.SoftLockItems.Contains(invItem.invItemName);
	}
}
