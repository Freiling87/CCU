using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
    public class Permanent_Hire : T_HireDuration
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Permanent_Hire>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = 
                        "Adds a 'Hire Permanently' interaction option at 8x the normal rate.\n\n" + 
                        "Permanent Hires can use their Expert ability unlimited times, and will ignore Homesickness.\n\n" + 
                        "<color=red>Requires:</color> Any Hire Type trait",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Permanent_Hire)),
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
