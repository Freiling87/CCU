using CCU.Traits.App_BC1;
using RogueLibsCore;

namespace CCU.Traits.App_BC3
{
	public class Shirtsome : T_BodyColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Shirtsome>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Prevents body from matching skin color.",
					[LanguageCode.Spanish] = "Evita que el color de cuerpo sea el mismo que el color de piel.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shirtsome)),
					[LanguageCode.Spanish] = "Al Des-Desnudo",
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
