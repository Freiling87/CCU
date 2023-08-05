using RogueLibsCore;

namespace CCU.Traits.Senses.Hearing_Range
{
	internal class Owl_Ears : T_HearingRange
    {
		internal override float hearingRange => 200f;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Owl_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Hearing range = 200%",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Owl_Ears)),
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