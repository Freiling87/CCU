using RogueLibsCore;

namespace CCU.Traits.Senses.Hearing_Range
{
	internal class Snake_Ears : T_HearingRange
    {
		internal override float hearingRange => 10f;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Snake_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Wish you could hear how cool that sounds. Hearing range = 10%",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Snake_Ears)),
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

		internal override bool canHearNoise(Noise noise)
		{
			throw new System.NotImplementedException();
		}
	}
}
