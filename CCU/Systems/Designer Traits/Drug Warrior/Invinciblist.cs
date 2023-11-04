using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class Invincibilist : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Invincible;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Invincibilist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will become Invincible upon entering combat. And yes, it's spelled right."),
					[LanguageCode.Spanish] = "Este NPC se volvera Invencible al entrar en combate, olvidate del daño inoportuno",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Invincibilist)),
					[LanguageCode.Spanish] = "Invensiblelista",

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
