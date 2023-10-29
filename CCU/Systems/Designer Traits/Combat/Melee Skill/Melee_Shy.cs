using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Melee_Shy : T_MeleeSkill
	{
		public override int MeleeSkill => 0;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Melee_Shy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Lowest attack frequency, like Comedian, Hacker & Zombie."),
					[LanguageCode.Spanish] = "Frequencia de ataque mas baja, como Comediante, Hacker o Zombie.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Melee_Shy)),
					[LanguageCode.Spanish] = "Lento al Garrote",

				})
				.WithUnlock(new TU_DesignerUnlock
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
