using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Choochootations : T_AmbientAudio
	{
		public override string ambientAudioClipName => "Train";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Choochootations>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "A term that refers to the expression of choo choo noises. My guess is autism."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Choochootations)),
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