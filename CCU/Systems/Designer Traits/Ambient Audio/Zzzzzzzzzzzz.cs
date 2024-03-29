﻿using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	public class Zzzzzzzzzzzz : T_AmbientAudio
	{
		public override string ambientAudioClipName => "LampPostAmbience";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Zzzzzzzzzzzz>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Makes Streetlamp noise."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Zzzzzzzzzzzz)),
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
