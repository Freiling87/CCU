using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class Little_Steep : T_CostScale
	{
		public override float CostScale => 1.25f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Little_Steep>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character's costs are increased by 25%.",
					[LanguageCode.Spanish] = "Los Precios de este NPC son Aumentados por 25%.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Little_Steep)),
					[LanguageCode.Spanish] = "Un Poco Aumentado",

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
