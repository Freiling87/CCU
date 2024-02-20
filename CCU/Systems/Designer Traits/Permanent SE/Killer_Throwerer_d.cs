using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Killer_Throwerer_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.KillerThrower;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Killer_Throwerer_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You permanently killer thrower thingers. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Killer_Throwerer_d), "Killer Throwerer [D]")
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