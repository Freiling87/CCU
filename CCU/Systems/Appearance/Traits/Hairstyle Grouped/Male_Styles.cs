using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS2
{
	public class Male_Styles : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "Military", "Normal", "NormalHigh", "Suave", "Pompadour", "Spiky", "SpikyShort", "Mohawk", "Bald", "Balding", "Sidewinder", "Curtains", "Afro", "PuffyShort" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Male_Styles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios peinado a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Male_Styles)),
					[LanguageCode.Spanish] = "Peinados Masculinos",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}