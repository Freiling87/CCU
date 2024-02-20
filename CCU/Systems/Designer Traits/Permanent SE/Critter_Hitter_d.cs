using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Critter_Hitter_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.AlwaysCrit;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Critter_Hitter_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You always get critical hits. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Critter_Hitter_d), "Critter Hitter [D]")
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