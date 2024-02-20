using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	public class Grab_Food : T_ItemPickup
	{
		public override string[] GrabItemCategories => new string[] { VItemCategory.Food };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Grab_Food>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab food if they see it."),
					[LanguageCode.Spanish] = "Este NPC agarra toda comida en el suelo que vea. porque es un cochino, UN COCHINO!!!.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Food)),
					[LanguageCode.Spanish] = "Colleciona Comida",
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
