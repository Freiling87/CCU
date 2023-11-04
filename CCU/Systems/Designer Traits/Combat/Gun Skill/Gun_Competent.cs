using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Gun_Competent : T_GunSkill
	{
		public override int GunSkill => 1;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Gun_Competent>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Average shooting frequency, like a Cop or gangster."),
					[LanguageCode.Spanish] = "Este NPC dispara armas con frequencia, como un Policía o Gangster.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Gun_Competent)),
					[LanguageCode.Spanish] = "Facil al Gatillo",

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
