using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	public class Have : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Have>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent will never fail to generate a pocket item, if there are any added to their item pool.",
					[LanguageCode.Spanish] = "NPC siempre tendra un item de bolsillo, si tiene uno en la lista.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Have)),
					[LanguageCode.Spanish] = "Tiene",
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
