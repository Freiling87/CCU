using CCU.Traits.Hairstyle;
using RogueLibsCore;

namespace CCU.Traits.Hairstyle_Grouped
{
    public class Not_Hairstyles : T_Hairstyle
	{
		public override string[] HairstyleType => new string[] { "SlavemasterMask", "AssassinMask", "Hoodie", "GorillaHead", "RobotHead", "HologramHead", "WerewolfHead", "ButlerBotHead", "CopBotHead", "AlienHead", "RobotPlayerHead", };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Not_Hairstyles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Not_Hairstyles)),
				})
				.WithUnlock(new TraitUnlock
				{
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