using BunnyLibs;
using CCU.Traits.Interaction;
using RogueLibsCore;

namespace CCU.Systems.Language
{
	public class Teach_Languages : T_Teacher, IBranchInteractionMenu
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => Learn_Language;
		public override bool HideCostInButton => true;
		public override string DetermineMoneyCostID => null;

		public const string
			Learn_Language = "Learn_Language",
			Learn_Binary = "Learn_Binary",
			Learn_Chthonic = "Learn_Chthonic",
			Learn_English = "Learn_English",
			Learn_ErSdtAdt = "Learn_ErSdtAdt",
			Learn_Foreign = "Learn_Foreign",
			Learn_Goryllian = "Learn_Goryllian",
			Learn_Undercant = "Learn_Undercant",
			Learn_Werewelsh = "Learn_Werewelsh";

		public bool ButtonCanShow(Agent interactingAgent) =>
			CoreTools.ContainsAll(LanguageSystem.KnownLanguagesWithoutTranslator(Owner, false), LanguageSystem.KnownLanguagesWithoutTranslator(interactingAgent, false));
		public InteractionState interactionState => InteractionState.LearnTraits_Language;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Teach_Languages>()
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
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});

			RogueLibs.CreateCustomName(Learn_Language, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Language",
			});

			foreach (string language in LanguageSystem.AllLanguages)
				RogueLibs.CreateCustomName($"Learn_{language}", NameTypes.Interface, new CustomNameInfo
				{
					[LanguageCode.English] = $"Learn {language}",
				});
		}


		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}