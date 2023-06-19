using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	internal class Melee_Shy : T_MeleeSkill
	{
		internal override int MeleeSkill => 0;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Melee_Shy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Lowest attack frequency, like Comedian, Hacker & Zombie."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Melee_Shy)),

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
