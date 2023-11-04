using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Powering_Noises : T_AmbientAudio
	{
		public override string ambientAudioClipName => "PowerBox";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Powering_Noises>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a Power Box noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Powering_Noises)),
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
