using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Sawblade_Sound : T_AmbientAudio
	{
		public override string ambientAudioClipName => "SawBladeRun";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sawblade_Sound>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a sawblade noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sawblade_Sound)),
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
