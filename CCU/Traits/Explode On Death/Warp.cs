using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Explode_On_Death
{
	public class Warp : T_ExplodeOnDeath
	{
		public override string ExplosionType => VExplosionType.Warp;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Warp>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("All the good ancient curses were taken. This one teleports anyone present for its holder's death to a mildly inconvenient location nearby. OoOoOoOh!"),
					[LanguageCode.Spanish] = "Al morir este NPC te manda al reino de las sombras conocido como (quotes)Las Streets de Rogue(quotes) el cual esta.... a unos pies de donde estas parado ahora, ooOOOoOOo que miedo!!!",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Warp)),
					[LanguageCode.Spanish] = "Teletransporte",

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