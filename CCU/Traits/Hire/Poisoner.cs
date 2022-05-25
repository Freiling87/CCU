using RogueLibsCore;

namespace CCU.Traits.Hire
{
    public class Poisoner : T_Hire
    {
        public override string ButtonText => CJob.Poison;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Poisoner>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to poison an air vent or water pump.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Poisoner)),
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
