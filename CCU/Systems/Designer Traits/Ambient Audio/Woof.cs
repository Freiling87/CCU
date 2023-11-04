using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Woof : T_AmbientAudio
	{
		public override string ambientAudioClipName => "FireConstant";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Woof>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes Flame Vent noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Woof)),
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
