using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
    internal class FunnyPack_Extreme : T_Loadout
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<FunnyPack_Extreme>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent can spawn with 2 more Pocket items.\n\n" + 
					"<i>Hey, FUCK YOU! BUY FunnyPack Extreme©! Extreme Mil-Spec Ancient Alien Interdimensional technology brings you A REALLY BIG FANNYPACK. EXTREEEEME!</i>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(FunnyPack_Extreme)),
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