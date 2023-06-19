using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	internal class Gun_Adept : T_GunSkill
	{
		internal override int GunSkill => 2;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Gun_Adept>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Highest shooting frequency, like Killer Robot, Soldier & Supercop."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Gun_Adept)),

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
    }
}
