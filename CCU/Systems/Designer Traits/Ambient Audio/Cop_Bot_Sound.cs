using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Cop_Bot_Sound : T_AmbientAudio
	{
		public override string ambientAudioClipName => "CopBotCam";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cop_Bot_Sound>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes Cop Bot noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_Bot_Sound)),
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
		
		
	}
}