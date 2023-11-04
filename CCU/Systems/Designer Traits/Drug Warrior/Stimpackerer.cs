using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Stimpackerer : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.RegenerateHealthFaster;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Stimpackerer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will start regenerating health *quickly* upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC regenera rapidamente salud al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Stimpackerer)),
					[LanguageCode.Spanish] = "Supermedicado",

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
