using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Stomp : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Stomp;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Stomp>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("On death, this character stuns everyone nearby. Bad gas?"),
					[LanguageCode.Spanish] = "Al morir, este NPC aturde a los NPCs alrededor suyo, un poco redundante pero se entiende.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Stomp)),
					[LanguageCode.Spanish] = "Pisoton",

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