using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class Less : T_CostScale
	{
		public override float CostScale => 0.50f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Less>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character's costs are decreased by 50%.",
					[LanguageCode.Spanish] = "Los Precios de este NPC son reducidos por 50%.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Less)),
					[LanguageCode.Spanish] = "Barato",

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
