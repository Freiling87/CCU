using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
    public class Grab_Everything : T_Behavior, ISetupAgentStats
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => new string[] { VItemCategory.Alcohol, VItemCategory.Bomb, VItemCategory.Defense, VItemCategory.Drugs, VItemCategory.Food, VItemCategory.GunAccessory, VItemCategory.Guns, VItemCategory.Health, VItemCategory.Melee, VItemCategory.MeleeAccessory, VItemCategory.Money, VItemCategory.Movement, VItemCategory.NonStandardWeapons, VItemCategory.NonStandardWeapons2, VItemCategory.NonUsableTool, VItemCategory.NonViolent, VItemCategory.NotRealWeapons, VItemCategory.NPCsCantPickUp, VItemCategory.Passive, VItemCategory.Sex, VItemCategory.Social, VItemCategory.Stealth, VItemCategory.Supplies, VItemCategory.Technology, VItemCategory.Usable, VItemCategory.Weapons, VItemCategory.Weird};

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Grab_Everything>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format($"This character will grab any item they see.\n\n" +
                        "<color=green>{0}</color>: Will try to pick up armed traps.", LongishDocumentationName(typeof(AccidentProne))),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Grab_Everything)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }

        public void SetupAgentStats(Agent agent)
        {
            agent.losCheckAtIntervals = true;
        }
    }
}
