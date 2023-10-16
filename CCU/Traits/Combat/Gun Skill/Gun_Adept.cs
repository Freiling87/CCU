using RogueLibsCore;
using System;

namespace CCU.Traits.CombatGeneric
{
	public class Gun_Adept : T_GunSkill
	{
		public override int GunSkill => 2;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Gun_Adept>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Highest shooting frequency, like Killer Robot, Soldier & Supercop."),
					[LanguageCode.Spanish] = "Este NPC dispara armas con maxima frequencia, como un Robot Asesino, Soldado o Superpolícia.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gun_Adept)),
					[LanguageCode.Spanish] = "Adepto en Gatillo",

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
