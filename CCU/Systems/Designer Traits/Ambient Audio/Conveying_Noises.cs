using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Conveying_Noises : T_AmbientAudio
	{
		public override string ambientAudioClipName => "ConveyorBelt";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Conveying_Noises>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes conveyor belt noises. I don't know what that... conveys."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Conveying_Noises)),
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
