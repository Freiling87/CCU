using CCU.Traits.App_LC1;
using RogueLibsCore;

namespace CCU.Traits.App_LC3
{
	public class Pantiful : T_LegsColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pantiful>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Prevents legs from matching skin color.",
					[LanguageCode.Spanish] = "Evita que el color de las piernas sea el mismo que el de la piel.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pantiful)),
					[LanguageCode.Spanish] = "De Pantis",
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
