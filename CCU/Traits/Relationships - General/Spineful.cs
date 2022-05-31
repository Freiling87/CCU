using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
    public class Spineful : T_Rel_General
    {
        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Spineful>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character will never go Submissive.\n\n" + 
                    "Use case: Gang members running a protection racket and serving as guards for a business owner, instead of hired goons who just want to go home at the end of the day.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Spineful)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
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
