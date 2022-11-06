using RogueLibsCore;
using CCU.Localization;

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
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Assist Me",
			}); RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Assist Me",
			});
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Permanent, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
			});
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Permanent, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
			});
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Permanent_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
			});
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Permanent_Voucher, t, new CustomNameInfo
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
