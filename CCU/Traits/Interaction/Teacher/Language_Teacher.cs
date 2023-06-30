using CCU.Hooks;
using CCU.Localization;
using CCU.Traits.Interaction;
using RogueLibsCore;

namespace CCU.Traits.Teacher
{
	public class Language_Teacher : T_Teacher, IBranchInteractionMenu
    {
		public override bool AllowUntrusted => false;
		public override string ButtonID => CButtonText.Teach_Language;
        public override bool HideCostInButton => true;
        public override string DetermineMoneyCostID => null;

        public InteractionState interactionState => InteractionState.TeachTraits_Language;

		//[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Language_Teacher>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Teaches any language they know.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Language_Teacher)),
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