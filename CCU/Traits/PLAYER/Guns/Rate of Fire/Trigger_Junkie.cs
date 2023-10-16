using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
	public class Trigger_Junkie : T_RateOfFire
	{
		public override float CooldownMultiplier => 0.6f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Trigger_Junkie>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] =
					Core.designerEdition
						? "Fire rate cooldown decreased by 40%."
						: "Fire rate cooldown decreased by 40 %.\n\n" +
							"<color=yellow>NPCs:</color> Firing interval decreased by 40%",
					[LanguageCode.Spanish] =
					Core.designerEdition
						? "Velocidad de fuego aumentada por 40%."
						: "Velocidad de fuego aumentada por 40 %.\n\n" +
							"<color=yellow>NPCs:</color> Intervalo entre disparos reducido por 40%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Trigger_Junkie)),
					[LanguageCode.Spanish] = "Addicto al Gatillo",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 7,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
					Unlock =
					{
						categories = { VTraitCategory.Guns },
						isUpgrade = true,
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}