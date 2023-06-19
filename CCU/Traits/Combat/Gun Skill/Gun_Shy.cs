using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	internal class Gun_Shy : T_GunSkill
	{
		internal override int GunSkill => 0;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Gun_Shy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Generally reluctant to use guns."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Gun_Shy)),

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
