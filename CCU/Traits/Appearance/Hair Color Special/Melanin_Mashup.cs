using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Melanin_Mashup : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melanin_Mashup>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Normally, darker skin colors will turn hair black automatically. This disables that feature.",
					[LanguageCode.Spanish] = "Permite que el personaje pueda tener cualquier color de pelo no importe su tono de piel.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Melanin_Mashup)),
					[LanguageCode.Spanish] = "Melanina Revuelta",
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
