using CCU.Traits.App_LC1;
using RogueLibsCore;

namespace CCU.Traits.App_LC3
{
	public class Pantsuit : T_LegsColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Pantsuit>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes legs to match body color.",
					[LanguageCode.Spanish] = "Evita que el color de las piernas sea el mismo que el del cuerpo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pantsuit)),
					[LanguageCode.Spanish] = "De Pijama",
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
