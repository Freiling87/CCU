using CCU.Localization;
using CCU.Traits.Player.Language;
using RogueLibsCore;

namespace CCU.Traits.Interaction
{
	public class Teach_Languages : T_Teacher, IBranchInteractionMenu
    {
		public override bool AllowUntrusted => false;
		public override string ButtonID => CButtonText.Teach_Language;
        public override bool HideCostInButton => true;
        public override string DetermineMoneyCostID => null;

        public InteractionState interactionState => InteractionState.TeachTraits_Language;

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Teach_Languages>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Teaches any language they know, for a price." + 
                    "\n- English (removes Vocally Challenged) - $600" +
                    "\n- Other languages - $200",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Teach_Languages)),
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

		public bool ButtonCanShow(Agent interactingAgent) =>
            CoreTools.ContainsAll(Language.LanguagesKnown(Owner, false), Language.LanguagesKnown(interactingAgent, false));

        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}