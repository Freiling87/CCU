using CCU.Traits.Behavior;
using RogueLibsCore;
using System;

namespace CCU.Traits.LOS_Behavior
{
	public class Grab_Everything : T_ItemPickup
	{
		public override string[] GrabItemCategories => new string[] { VItemCategory.Alcohol, VItemCategory.Bomb, VItemCategory.Defense, VItemCategory.Drugs, VItemCategory.Food, VItemCategory.GunAccessory, VItemCategory.Guns, VItemCategory.Health, VItemCategory.Melee, VItemCategory.MeleeAccessory, VItemCategory.Money, VItemCategory.Movement, VItemCategory.NonStandardWeapons, VItemCategory.NonStandardWeapons2, VItemCategory.NonUsableTool, VItemCategory.NonViolent, VItemCategory.NotRealWeapons, VItemCategory.NPCsCantPickUp, VItemCategory.Passive, VItemCategory.Sex, VItemCategory.Social, VItemCategory.Stealth, VItemCategory.Supplies, VItemCategory.Technology, VItemCategory.Usable, VItemCategory.Weapons, VItemCategory.Weird };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Grab_Everything>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format($"This character will grab any item they see.\n\n" +
						"<color=green>{0}</color>: Will try to pick up armed traps.", DocumentationName(typeof(Accident_Prone))),
					[LanguageCode.Spanish] = String.Format($"Este NPC agarra TODO lo que encuentre, no voy a hacer un chiste facil aca.\n\n" +
					  "<color=green>{0}</color>: Intentaran agarrar trampas armadas.", DocumentationName(typeof(Accident_Prone))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Everything)),
					[LanguageCode.Spanish] = "Colleciona Todo",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
	}
}