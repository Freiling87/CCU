using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Generating_Overclocked_Sounds : T_AmbientAudio
	{
		public override string ambientAudioClipName => "OverclockedGeneratorAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Generating_Overclocked_Sounds>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes overclocked generator noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Generating_Overclocked_Sounds)),
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
