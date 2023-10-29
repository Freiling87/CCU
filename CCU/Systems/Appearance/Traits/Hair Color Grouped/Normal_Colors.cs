using CCU.Traits.App_HC1;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.App_HC2
{
	public class Normal_Colors : T_HairColor
	{
		public override string[] Rolls => StaticList.ToArray();
		public static List<string> StaticList = new List<string>() { "Brown", "Black", "Blonde", "Orange", "Grey" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Normal_Colors>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds normal human hair colors to the pool.",
					[LanguageCode.Spanish] = "Agrega varios colores comunes de pelo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_Colors)),
					[LanguageCode.Spanish] = "Colores Normales",
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