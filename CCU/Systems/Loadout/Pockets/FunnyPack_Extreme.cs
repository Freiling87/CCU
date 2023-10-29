using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	public class FunnyPack_Extreme : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<FunnyPack_Extreme>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent can spawn with 2 more Pocket items.\n\n" +
					"<i>Hey, FUCK YOU! BUY FunnyPack Extreme©! Extreme Mil-Spec Ancient Alien Interdimensional technology brings you A REALLY BIG FANNYPACK. EXTREEEEME!</i>",
					[LanguageCode.Spanish] = "NPC puede spawnear con 2 items de bollsillo addicionales.\n\n" +
					"<i>HOLA! Soy Bins con Riñowah, esto te va a facinar, mira mis huevos! la riñowah tiene espacio suficienta para guardarlos adentro, me los tiene frescos!</i>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(FunnyPack_Extreme)),
					[LanguageCode.Spanish] = "Riñonero Extremo",
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