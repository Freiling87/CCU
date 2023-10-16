using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class EMP : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.EMP;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<EMP>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character releases an EMP."),
					[LanguageCode.Spanish] = "Al morir, Este NPC explota como una granada de EMP, comerte los imanes de la heladera es peligroso y no te dara poderes dice el gobierno...",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(EMP)),
					[LanguageCode.Spanish] = "EMP",

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