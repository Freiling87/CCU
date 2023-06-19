using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	internal class Melee_Adept : T_MeleeSkill
	{
		internal override int MeleeSkill => 2;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Melee_Adept>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Highest attack frequency, like Gorilla, Supercop & Werewolf."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Melee_Adept)),

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
