using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC2
{
    public class Demographic_Colors : T_HairColor
	{
		public override string[] Rolls => new string[] { 
			"Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black",
			"Brown", "Brown", "Brown", "Brown", "Brown", "Brown",
			"Blonde", "Blonde", "Blonde",
			"Orange", };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Demographic_Colors>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple hair colors to the appearance pool, curated to more accurately resemble demographic tendencies:\n" +
					"- 50% Black\n" +
					"- 30% Brown\n" +
					"- 15% Blonde\n" +
					"- 5% Orange\n\n" +
					"This trait adds 20 items to the pool, so it's also a good way to make dyed or white hair less common if you add them to to the pool.",
                    [LanguageCode.Spanish] = "\"Agrega múltiples colores de cabello a los que el personaje puede usar, seleccionados para parecerse con mayor precisión a las tendencias demográficas:\n" +
                    "- 50% Negro\n" +
                    "- 30% Moreno\n" +
                    "- 15% Rubio\n" +
                    "- 5% Pelirrojo\n\n" +
                    "Este rasgo agrega 20 colores a la lista, asi que sirve para hacer los colores que adreges mas raros.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Demographic_Colors)),
                    [LanguageCode.Spanish] = "Colores Demograficos",
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