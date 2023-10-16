using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Suicide_Bomber_Normal : T_DrugWarrior
	{
		public override string DrugEffect => ""; // CStatusEffect.SuicideBomb;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Suicide_Bomber_Normal>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will die in a Normal explosion 15 seconds after starting combat."),
					[LanguageCode.Spanish] = "Este NPC explotara 15 segundos al entrar en combate, la explosion es equivalente a una granada.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suicide_Bomber_Normal), "Suicide Bomber (Normal)"),
					[LanguageCode.Spanish] = "Kamikaze (Normal)",

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
