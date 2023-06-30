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
                .WithName(new CustomNameInfo 
                {
					[LanguageCode.English] = "Rubber Bullets Mod"
                })
                .WithDescription(new CustomNameInfo
                {
					[LanguageCode.English] = "Knocks out targets... usually."
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
                [LanguageCode.English] = "When you die, you die poor. It really makes you think."
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_01", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Should have just complied!"
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_02", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Oh, now you stop resisting."
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_03", t, new CustomNameInfo
            {
                [LanguageCode.English] = "That's for making me slightly tired!"
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_04", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Better sprinkle some \"Sugar\" on that one."
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_05", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Another situation de-escalated!"
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_06", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Some damages are more collateral than others."
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_07", t, new CustomNameInfo
            {
                [LanguageCode.English] = "At least they can't sue the department if they're dead."
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_08", t, new CustomNameInfo
            {
                [LanguageCode.English] = "All Collaterals Are Bad!"
            });
            RogueLibs.CreateCustomName("LessLethalCollateral_09", t, new CustomNameInfo
            {
                [LanguageCode.English] = "Ugh, now I have to fill out a form. WHYYYYY?!"
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

        public static void ByTheBook(Agent agent)
		{
            string number = UnityEngine.Random.Range(0, 9).ToString("D2");
            agent.Say(GC.nameDB.GetName("LessLethalCollateral_" + number , "Dialogue"));
        }
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