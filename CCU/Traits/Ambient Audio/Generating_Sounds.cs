﻿using RogueLibsCore;

namespace CCU.Traits.Ambient_Audio
{
	internal class Generating_Sounds : T_AmbientAudio
	{
		internal override string ambientAudioClipName => "GeneratorAmbience";

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Generating_Sounds>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Makes generator noise."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Generating_Sounds)),
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
