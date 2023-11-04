using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	public class Have_Mostly : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Have_Mostly>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "25% chance to not generate with any Pocket items. For Scaled / Upscaled loaders, items must still roll their chance successfully to generate.\n\n" +
					"It turns out there are <i>three</i> kinds of people. Who knew?",
					[LanguageCode.Spanish] = "Chance del 25% de no generar un item de bolsillo. Con Distribucion Escalada los items tienen una chance menor de aparecer.\n\n" +
					"En este mundo hay <i>tres</i> tipos de personas. Las gente que si, la gente que no y las gente que eh talves, no se, a lo mejor mañana.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Have_Mostly)),
					[LanguageCode.Spanish] = "Tiende a Tener",
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
