using RogueLibsCore;

namespace CCU.Traits.App_HC1
{
	public class White_Hair : T_HairColor
	{
		public override string[] Rolls => new string[] { "White" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<White_Hair>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este color de pelo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(White_Hair)),
					[LanguageCode.Spanish] = "Pelo Blanco",
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