using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Generating_Overclocked_Sounds : T_AmbientAudio
	{
		public override string ambientAudioClipName => "OverclockedGeneratorAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Generating_Overclocked_Sounds>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes overclocked generator noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Generating_Overclocked_Sounds)),
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
