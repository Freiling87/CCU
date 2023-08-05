using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Ventulations : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "GasConstant";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Ventulations>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes a gas vent noise."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Ventulations)),
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
