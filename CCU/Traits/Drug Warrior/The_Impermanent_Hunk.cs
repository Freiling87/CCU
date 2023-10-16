using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class The_Impermanent_Hunk : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Strength;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<The_Impermanent_Hunk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will gain a *temporary* Strength buff upon entering combat.\n\n<color=red>Remember, kids: Don't maintain? Lose your gains!</color>"),
					[LanguageCode.Spanish] = "Este NPC gana un *buff temporal* de fuerza al entrar en combate.\n\n<color=red>Recuerden niños!: Tu tienes el poder! hasta la proxima chicos!</color>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(The_Impermanent_Hunk)),
					[LanguageCode.Spanish] = "El Impotente Hunk",

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