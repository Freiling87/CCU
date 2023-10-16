using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
	public class Zero : T_CostScale
	{
		public override float CostScale => 0.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Zero>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character's costs are reduced to zero.",
					[LanguageCode.Spanish] = "Los Precios de este NPC son Reducidos por 100%, lo barato es magico, y lo gratis lo es aun mas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Zero)),
					[LanguageCode.Spanish] = "Gratis",

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
