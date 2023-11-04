using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Melee_Adept : T_MeleeSkill
	{
		public override int MeleeSkill => 2;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Melee_Adept>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Highest attack frequency, like Gorilla, Supercop & Werewolf."),
					[LanguageCode.Spanish] = "Mayor frequencia de ataque, como Gorila, Superpolicía o Hombre Lobo.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Melee_Adept)),
					[LanguageCode.Spanish] = "Adepto al Garrote",

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
		
		
	}
}
