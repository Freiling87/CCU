using RogueLibsCore;

namespace CCU.Traits.LOS_Behavior
{
	public class Grab_Alcohol : T_ItemPickup
	{
		public override string[] GrabItemCategories => new string[] { VItemCategory.Alcohol };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Grab_Alcohol>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format("This character will grab alcohol if they see it."),
					[LanguageCode.Spanish] = "Este NPC agarra todo el alcohol suelto que encuentre en el suelo, como tu padre o tio de confianza.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Grab_Alcohol)),
					[LanguageCode.Spanish] = "Colleciona Alcohol",
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
