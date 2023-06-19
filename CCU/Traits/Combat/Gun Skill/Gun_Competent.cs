using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	internal class Gun_Competent : T_GunSkill
	{
		internal override int GunSkill => 1;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Gun_Competent>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Average shooting frequency, like a Cop or gangster."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Gun_Competent)),

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
