using RogueLibsCore;

namespace CCU.Traits.Hire
{
    public class Safecracker : T_Hire
    {
        public override string ButtonText => CJob.SafecrackSafe;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Safecracker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to break into safes up-close and silently.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Safecracker)),
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
