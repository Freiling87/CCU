using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	public class Grab_Money : T_ItemPickup
	{
		public override string[] GrabItemCategories => new string[] { VItemCategory.Money };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Grab_Money>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab money if they see it."),
					[LanguageCode.Spanish] = "Este NPC agarra el dinero facil como todo un oportunista inversor.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Money)),
					[LanguageCode.Spanish] = "Colleciona Dinero",
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
