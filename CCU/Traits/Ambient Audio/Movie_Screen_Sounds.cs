﻿using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Movie_Screen_Sounds : T_AmbientAudio
	{
		public override string ambientAudioClipName => "MovieScreen";

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Movie_Screen_Sounds>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes a movie screen noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Movie_Screen_Sounds)),
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
