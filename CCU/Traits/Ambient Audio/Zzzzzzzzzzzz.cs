using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Zzzzzzzzzzzz : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "LampPostAmbience";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Zzzzzzzzzzzz>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes Streetlamp noise."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Zzzzzzzzzzzz)),
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
