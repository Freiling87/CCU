using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public class Rubber_Bulleteer : T_GunNut
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		public override string[] AddedItemCategories => new string[] { VItemCategory.NonViolent };
		public override string GunMod => VItemName.RubberBulletsMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			VItemName.FireExtinguisher,
			VItemName.Flamethrower,
			VItemName.FreezeRay,
			VItemName.GhostGibber,
			VItemName.Leafblower,
			VItemName.OilContainer,
			VItemName.ResearchGun,
			VItemName.RocketLauncher,
			VItemName.ShrinkRay,
			VItemName.Taser,
			VItemName.TranquilizerGun,
			VItemName.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Rubber_Bulleteer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Rubber Bullets Mod to all traditional firearms in inventory. If rubber bullet damage reduces an NPC's health to 10% or lower, they are knocked out. If they go below -10%, they are killed... but less lethally! Rubber Bullet guns are usable by Pacifists.",
					[LanguageCode.Spanish] = "Aplica Balas de Goma a todas las armas tradicionales. Recuerda que esto hace que las armas noqueen a los NPCs cuando estan pordebajo de 10% de salud, pero si de alguna manera te pasas de eso mueren menos letalmente, pacifistas pueden usar estas armas.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Rubber_Bulleteer)),
					[LanguageCode.Spanish] = "El Pacificador",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 10,
					IsAvailable = true,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 16,
					Unlock =
					{
						categories = { VTraitCategory.Guns }
					}
				});

			// Replaces vanilla trait text
			RogueLibs.CreateCustomName(VanillaTraits.Pacifist, NameTypes.Description, new CustomNameInfo
			{
				[LanguageCode.English] = "Can't use weapons, except for some thrown items and Rubber Bullet-modded guns.",
				[LanguageCode.Spanish] = "No puedes usar armas, excepto algunos objetos arrojadizos y armas no-letales incluyendo las modificadas.",
			});
		}
	}
}