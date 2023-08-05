using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Squeakitations : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "MineCart";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Squeakitations>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes a mine cart noise."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Squeakitations)),
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
