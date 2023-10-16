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
			PostProcess = RogueLibs.CreateCustomTrait<Male_Styles>()
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
	}
}