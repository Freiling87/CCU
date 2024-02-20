using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class LyCANthrope_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Werewolf;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<LyCANthrope_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "There's nothing you can't do. You're a werewolf forever. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(LyCANthrope_d), "LyCANthrope [D]")
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