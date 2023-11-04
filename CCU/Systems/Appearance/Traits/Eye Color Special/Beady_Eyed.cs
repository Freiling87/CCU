using CCU.Traits.App_EC1;
using RogueLibsCore;

namespace CCU.Traits.App_EC3
{
	public class Beady_Eyed : T_EyeColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Beady_Eyed>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Matches Eye Color to Skin Color.",
					[LanguageCode.Spanish] = "Hace que el color de los ojos coincida con el color de la piel.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Beady_Eyed), "Beady-Eyed"),
					[LanguageCode.Spanish] = "Ojitos Chiquitos",
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
