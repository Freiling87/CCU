using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class Less : T_CostScale
	{
		public override float CostScale => 0.50f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Less>()
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
