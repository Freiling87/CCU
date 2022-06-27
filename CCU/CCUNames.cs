using RogueLibsCore;
using SORCE.Localization;

namespace CCU
{
    public static class CCUNames
	{
		[RLSetup]
		public static void InitializeNames()
		{
			string t;
			//	Button Text
			t = NameTypes.Interface;
			RogueLibs.CreateCustomName(CButtonText.HirePermanentExpert, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
			});
			RogueLibs.CreateCustomName(CButtonText.HirePermanentMuscle, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
			});

			//	Jobs
			t = NameTypes.Interface;
			RogueLibs.CreateCustomName(CJob.DisarmTrap, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Disarm Trap",
			});
			RogueLibs.CreateCustomName(CJob.Pickpocket, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Pickpocket",
			});
			RogueLibs.CreateCustomName(CJob.Poison, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Poison",
			});
			RogueLibs.CreateCustomName(CJob.SafecrackSafe, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Safecrack",
			});
			RogueLibs.CreateCustomName(CJob.TamperSomething, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Tamper",
			});
		}
	}
}
