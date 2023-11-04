using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Big : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Big;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Big>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character explodes. About 4 Slaves' worth. "),
					[LanguageCode.Spanish] = "Al morir, este NPC explota en el equivalente de 4 esclavos dandose un graaaan abrazo :).",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Big)),
					[LanguageCode.Spanish] = "Grande",

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