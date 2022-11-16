using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Rate_of_Fire_Mod : T_GunNut
    {
		public override string GunMod => vItem.RateofFireMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.OilContainer,
			vItem.ResearchGun,
			vItem.Taser,
			vItem.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Rate_of_Fire_Mod>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Rate of Fire Mod to all ranged weapons in inventory.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Rate_of_Fire_Mod)),
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