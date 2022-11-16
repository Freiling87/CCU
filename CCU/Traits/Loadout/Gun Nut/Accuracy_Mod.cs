using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Accuracy_Mod : T_GunNut
    {
		public override string GunMod => vItem.AccuracyMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.OilContainer, 
			vItem.ResearchGun,
			vItem.WaterPistol,
		};

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Accuracy_Mod>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Accuracy Mod to all ranged weapons in inventory.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Accuracy_Mod)),
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