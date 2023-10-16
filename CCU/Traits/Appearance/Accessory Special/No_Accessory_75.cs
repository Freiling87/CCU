using CCU.Traits.App_AC1;
using RogueLibsCore;

namespace CCU.Traits.App_AC3
{
	public class No_Accessory_75 : T_Accessory
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<No_Accessory_75>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"No Accessory\" 75% of the time, regardless of the number of other items in the pool.",
					[LanguageCode.Spanish] = "Pone una chance de tener \"Sin Accesorio\" de 75% no importe cuantos rasgos de accesorio tenga",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(No_Accessory_75), "No Accessory 75%"),
					[LanguageCode.Spanish] = "Sin Accesorio 75%",
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
