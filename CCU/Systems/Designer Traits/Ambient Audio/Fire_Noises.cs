using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Fire_Noises : T_AmbientAudio
	{
		public override string ambientAudioClipName => "FlamingBarrelCrackle";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fire_Noises>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a flame crackle noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fire_Noises)),
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
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
