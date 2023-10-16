using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC2
{
	public class Wild_Colors : T_HairColor
	{
		public override string[] Rolls
			=> new string[] { "Red", "Green", "Purple", "Pink", "Blue" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Wild_Colors>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hair colors to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega varios colores raros de pelo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wild_Colors)),
					[LanguageCode.Spanish] = "Colores Locos",
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