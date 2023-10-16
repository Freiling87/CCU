using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Confusionist : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Confused;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Confusionist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will become Confused upon entering combat. Maybe it's a trick? A ploy? A ruse...?\n\nNo, there's actually something wrong with them."),
					[LanguageCode.Spanish] = "Este NPC se confunde al entrar en combate. Aveces el cerebro simplemente se desconecta sabes?",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Confusionist)),
					[LanguageCode.Spanish] = "Confundisionado",

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
