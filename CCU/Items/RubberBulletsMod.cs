using BepInEx.Logging;
using CCU.Localization;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Items
{
    [ItemCategories(RogueCategories.Weapons, RogueCategories.GunAccessory, RogueCategories.Guns)]
    public class RubberBulletsMod : I_CCU, IItemCombinable
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [RLSetup]
        public static void Setup()
        {
            ItemBuilder itemBuilder = RogueLibs.CreateCustomItem<RubberBulletsMod>()
                .WithName(new CustomNameInfo("Rubber Bullets Mod"))
                .WithDescription(new CustomNameInfo("If the attached weapon reduces a target's health to 20% or lower, they are knocked out. If they go below -10%, they are killed... but less lethally! Rubber Bullet guns are usable by Pacifists."))
                .WithSprite(Properties.Resources.RubberBulletsMod)
                .WithUnlock(new ItemUnlock
                {
                    CharacterCreationCost = 3,
                    IsAvailable = true,
                    LoadoutCost = 5,
                    UnlockCost = 15,
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
            other.itemType == "WeaponProjectile" && !other.contents.Contains(vItem.RubberBulletsMod) &&
                (other.invItemName == vItem.Pistol || other.invItemName == vItem.Shotgun || other.invItemName == vItem.MachineGun || other.invItemName == vItem.Revolver);
        public bool CombineItems(InvItem other)
        {
            if (CombineFilter(other))
            {
                other.contents.Add(vItem.RubberBulletsMod);
                Instance.Categories.Add(VItemCategory.NonViolent);
                Instance.Categories.Add(VItemCategory.NotRealWeapons);
                Item.agent.agentInvDatabase.SubtractFromItemCount(Item.agent.agentInvDatabase.FindItem(vItem.RubberBulletsMod), 1);
                Item.agent.mainGUI.invInterface.HideDraggedItem();
                Item.agent.mainGUI.invInterface.HideTarget();
                GC.audioHandler.Play(Item.agent, VanillaAudio.CombineItem);

                return true;
            }

            return false;
        }
        public CustomTooltip CombineTooltip(InvItem other) => default;
    }

	[HarmonyPatch(declaringType: typeof(InvDatabase))]
    public static class P_InvDatabase_RubberBulletsMod
    { 
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.DetermineIfCanUseWeapon))]
        public static bool DetermineIfCanUseWeapon_AllowRubberBullets(InvDatabase __instance, InvItem item, ref bool __result)
		{
            if (item.contents.Contains(vItem.RubberBulletsMod) && __instance.agent.HasTrait(VanillaTraits.Pacifist))
			{
                __result = true;
                return false;
            }

            return true;
		}

    }
}