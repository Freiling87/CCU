using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Whhhhhhhh : T_AmbientAudio
	{
		public override string ambientAudioClipName => "AirFiltrationAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Whhhhhhhh>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes Air Conditioner noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Whhhhhhhh)),
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
