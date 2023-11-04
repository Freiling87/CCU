using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Wummmmmm : T_AmbientAudio
	{
		public override string ambientAudioClipName => "LaserAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Wummmmmm>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes Laser Emitter noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wummmmmm)),
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
