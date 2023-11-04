using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
	public class An_Inimitable_Bulk : T_DrugWarrior
	{
		public override string DrugEffect => VStatusEffect.Giant;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<An_Inimitable_Bulk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character gains Giant when entering combat. They're angry because they can't pronounce the name of this trait out loud. And, I presume, you will not like them when they are angry."),
					[LanguageCode.Spanish] = "Este NPC se vuelve Gigante al entrar en combate, Estan enojoados que nadie les crea nada, es realmente una vida tragica sabes?",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(An_Inimitable_Bulk)),
					[LanguageCode.Spanish] = "El Imprensindible Bulk",

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
