using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Wummmmmm : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "LaserAmbience";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Wummmmmm>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes Laser Emitter noise."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Wummmmmm)),
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
