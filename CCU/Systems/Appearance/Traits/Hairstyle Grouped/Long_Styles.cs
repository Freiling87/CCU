using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS2
{
	public class Long_Styles : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "BangsLong", "BangsMedium", "FlatLong", "MessyLong", "Wave", "PuffyLong" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Long_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios peinado a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Long_Styles)),
					[LanguageCode.Spanish] = "Peinados Largos",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}