using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.PSE
{
	public class Dying_d : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Poisoned;

		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Dying_d>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are dying. Oh, your bucket list consists entirely of killing people? How nice. Designer copy of ResistanceHR player trait added by request.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Dying_d), "Dying [D]")
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