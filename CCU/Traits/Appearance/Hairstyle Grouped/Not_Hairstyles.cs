using CCU.Traits.App_HS1;
using RogueLibsCore;

namespace CCU.Traits.App_HS2
{
    public class Not_Hairstyles : T_Hairstyle
	{
        public override string[] Rolls => 
			new string[] { "SlavemasterMask", "AssassinMask", "Hoodie", "GorillaHead", "RobotHead", "WerewolfHead", "ButlerBotHead", "CopBotHead", "AlienHead", "RobotPlayerHead" };

		public static string[] StaticList => 
			new string[] { "SlavemasterMask", "AssassinMask", "Hoodie", "GorillaHead", "RobotHead", "HologramHead", "WerewolfHead", "ButlerBotHead", "CopBotHead", "AlienHead", "RobotPlayerHead" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Not_Hairstyles>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool. I mean, you can use it but... what the hell are you making?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Not_Hairstyles)),
				})
				.WithUnlock(new TraitUnlock_CCU
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