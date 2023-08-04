using CCU.Traits.Player.Status_Effect;
using RogueLibsCore;

namespace CCU.Traits.Senses
{
	internal class Deaf : T_Hearing
    {
		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Deaf>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "They're deaf. DEAF.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Deaf)),
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
