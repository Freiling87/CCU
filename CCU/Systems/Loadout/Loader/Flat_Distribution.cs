using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Flat_Distribution : T_LoadoutLoader
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Flat_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Randomly selects an item for each pool (including pockets), with a (1/N+1)% chance to generate no item, where N is the number of items in the pool.",
					[LanguageCode.Spanish] = "Elige aleatoriamente un item de cada lista (incluyendo bolsillos), con una chance de (1/N+1)% de no generar un item, N siendo el numero de items en la lista.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Flat_Distribution)),
					[LanguageCode.Spanish] = "Distribucion Plana",
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