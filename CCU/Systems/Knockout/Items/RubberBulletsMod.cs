using BepInEx.Logging;
using BunnyLibs;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Items
{
	[ItemCategories(RogueCategories.Weapons, RogueCategories.GunAccessory, RogueCategories.Guns)]
	public class RubberBulletsMod : I_CCU, IItemCombinable
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		public static void Setup()
		{
			ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<RubberBulletsMod>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Rubber Bullets Mod",
					[LanguageCode.Spanish] = "Balas de Goma",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If the attached weapon reduces a target's health to 10% or lower, they are knocked out. If they go below -10%, they are killed... but less lethally! Rubber Bullet guns are usable by Pacifists.",

					[LanguageCode.Spanish] = "Con esto las balas noquean a la chusma de manera no-letal si tienen la salud baja (10% de su salud para ser exactos), pero si el daño es exesivo y los manda al -10% moriran de una manera ligeramente menos letal, Armas con esta modificacion son aptas para Pacifistas.",
				})
				.WithSprite(Properties.Resources.RubberBulletsMod)
				.WithUnlock(new ItemUnlock
				{
					CharacterCreationCost = 3,
					IsAvailable = true,
					LoadoutCost = 5,
					UnlockCost = 15,
				});

			string t = NameTypes.Dialogue;

			RogueLibs.CreateCustomName("LessLethalCollateral_00", t, new CustomNameInfo
			{
				[LanguageCode.English] = "When you die, you die poor. It really makes you think.",
				[LanguageCode.Spanish] = "Morite pobre, ay, se me salio otra ves",
			});
			RogueLibs.CreateCustomName("LessLethalCollateral_01", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Should have just complied!",
				[LanguageCode.Spanish] = "Solo seguía ordenes"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_02", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Oh, <i>now</i> you stop resisting.",
				[LanguageCode.Spanish] = "No resista! Pare de resistir! Sigue resistiendo?!"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_03", t, new CustomNameInfo
			{
				[LanguageCode.English] = "That's for making me slightly tired!",
				[LanguageCode.Spanish] = "No me pagan para correr tanto"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_04", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Better sprinkle some \"Sugar\" on that one.",
				[LanguageCode.Spanish] = "Metalen la merca y a disimular compis"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_05", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Another situation de-escalated!",
				[LanguageCode.Spanish] = "otro dia salvado por las fuerzas policiacas"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_06", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Some damages are more collateral than others.",
				[LanguageCode.Spanish] = "Digamos que fue un colateral, o autodefensa, lo que se vea mejor"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_07", t, new CustomNameInfo
			{
				[LanguageCode.English] = "At least they can't sue the department if they're dead.",
				[LanguageCode.Spanish] = "Ya no me pueden acusar ni denunciar al menos"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_08", t, new CustomNameInfo
			{
				[LanguageCode.English] = "All Collaterals Are Bad!",
				[LanguageCode.Spanish] = "Estos gatillos están más complicados cada día"
			});

			RogueLibs.CreateCustomName("LessLethalCollateral_09", t, new CustomNameInfo
			{
				[LanguageCode.English] = "Ugh, now I have to fill out a form.",
				[LanguageCode.Spanish] = "Tanto papeleo por un chiquito accidente de descarga? no podes!!! no podes!!!"
			});
		}

		public override void SetupDetails()
		{
			Item.itemType = ItemTypes.Combine;
			Item.itemValue = 120;
			Item.initCount = 1;
			Item.rewardCount = 1;
			Item.stackable = true;
			Item.Categories = new List<string> { VItemCategory.Weapons, VItemCategory.GunAccessory, VItemCategory.Guns };
		}

		public CustomTooltip CombineCursorText(InvItem other) => default;
		public bool CombineFilter(InvItem other) =>
			other.itemType == "WeaponProjectile" && !other.contents.Contains(VItemName.RubberBulletsMod) &&
				(other.invItemName == VItemName.Pistol || other.invItemName == VItemName.Shotgun || other.invItemName == VItemName.MachineGun || other.invItemName == VItemName.Revolver);
		public bool CombineItems(InvItem other)
		{
			if (CombineFilter(other))
			{
				other.contents.Add(VItemName.RubberBulletsMod);
				Instance.Categories.Add(VItemCategory.NonViolent);
				Instance.Categories.Add(VItemCategory.NotRealWeapons);
				Item.agent.agentInvDatabase.SubtractFromItemCount(Item.agent.agentInvDatabase.FindItem(VItemName.RubberBulletsMod), 1);
				Item.agent.mainGUI.invInterface.HideDraggedItem();
				Item.agent.mainGUI.invInterface.HideTarget();
				GC.audioHandler.Play(Item.agent, VanillaAudio.CombineItem);

				return true;
			}

			return false;
		}
		public CustomTooltip CombineTooltip(InvItem other) => default;

		public static void SayDialogue(Agent agent)
		{
			string number = UnityEngine.Random.Range(0, 9).ToString("D2");
			agent.Say(GC.nameDB.GetName("LessLethalCollateral_" + number, "Dialogue"));
		}
	}
}