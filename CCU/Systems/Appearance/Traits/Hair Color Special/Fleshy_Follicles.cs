using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Fleshy_Follicles : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fleshy_Follicles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Matches hair color to skin color.",
					[LanguageCode.Spanish] = "Hace que el color de pelo y el color de piel sean iguales.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fleshy_Follicles)),
					[LanguageCode.Spanish] = "Pelado Peinado",
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
