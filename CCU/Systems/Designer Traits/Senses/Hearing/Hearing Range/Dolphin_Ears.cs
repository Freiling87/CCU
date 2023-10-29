using RogueLibsCore;

namespace CCU.Traits.Senses.Hearing_Range
{
	internal class Dolphin_Ears : T_HearingRange
    {
		internal override float hearingRange => 100;

        internal override bool canHearNoise(Noise noise)
        {
            //if(gc.tileInfo.WaterNearby()) etc

            return true;
        }

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Dolphin_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Hear at double range through water.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Dolphin_Ears)),
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