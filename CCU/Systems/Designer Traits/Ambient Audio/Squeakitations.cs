using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Squeakitations : T_AmbientAudio
	{
		public override string ambientAudioClipName => "MineCart";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Squeakitations>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a mine cart noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Squeakitations)),
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
