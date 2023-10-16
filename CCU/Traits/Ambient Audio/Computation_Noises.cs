using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Computation_Noises : T_AmbientAudio
	{
		public override string ambientAudioClipName => "ComputerAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Computation_Noises>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a Computer noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Computation_Noises)),
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
