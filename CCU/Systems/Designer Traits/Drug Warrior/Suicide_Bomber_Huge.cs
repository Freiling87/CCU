using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Suicide_Bomber_Huge : T_DrugWarrior
	{
		public override string DrugEffect => ""; // CStatusEffect.SuicideBomb;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Suicide_Bomber_Huge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will die in a Huge explosion 15 seconds after starting combat."),
					[LanguageCode.Spanish] = "Este NPC explotara 15 segundos al entrar en combate, la explosion es equivalente a un generador acelerado.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Suicide_Bomber_Huge), "Suicide Bomber (Huge)"),
					[LanguageCode.Spanish] = "Kamikaze (Enorme)",

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
