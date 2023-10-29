using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class Less_ish : T_CostScale
	{
		public override float CostScale => 0.75f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Less_ish>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character's costs are decreased by 25%.",
					[LanguageCode.Spanish] = "Los Precios de este NPC son reducidos por 25%.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Less_ish), "Less-ish"),
					[LanguageCode.Spanish] = "Poco Barato",

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
