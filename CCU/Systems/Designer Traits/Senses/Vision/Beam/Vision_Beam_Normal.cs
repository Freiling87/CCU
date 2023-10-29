using CCU.Traits.Ambient_Audio;
using RogueLibsCore;
using System.Linq;

namespace CCU.Traits.Senses.Vision
{
	internal class Vision_Beam_Normal : T_VisionBeam
	{
		internal override string ParticleEffectType => "SecurityCamBeam"; // Untested

		//[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Vision_Beam_Normal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Purple vision beam.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Vision_Beam_Normal)),
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