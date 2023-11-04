using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Recoverist : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.StableSystem;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Recoverist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will gain Stable System upon entering combat."),
					[LanguageCode.Spanish] = "Este NPC obtiene un Sistema Estable al entrar en combate.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Recoverist)),
					[LanguageCode.Spanish] = "Preventivo",

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
