using RogueLibsCore;

namespace CCU.Traits.Senses.Hearing_Range
{
	internal class Hack_Sensor : T_HearingRange
    {
		internal override float hearingRange => 100;

        internal override bool canHearNoise(Noise noise)
        {
            // Detects hacks

            return true;
        }

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Hack_Sensor>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Detect hacks.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Hack_Sensor)),
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