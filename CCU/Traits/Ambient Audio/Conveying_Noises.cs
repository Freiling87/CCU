using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Conveying_Noises : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "ConveyorBelt";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Conveying_Noises>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes conveyor belt noises. I don't know what that... conveys."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Conveying_Noises)),
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
