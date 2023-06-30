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
			//	Interaction Button Text
			t = NameTypes.Interface; 
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Assist Me",
			}); 
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Voucher, t, new CustomNameInfo
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
			RogueLibs.CreateCustomName(CButtonText.Teach_Binary, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Binary",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Chthonic, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Chthonic",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_English, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn English",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_ErSdtAdt, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn ErSdtAdt",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Foreign, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Foreign",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Language, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn a Language",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Goryllian, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Goryllian",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Undercant, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Undercant",
			});
			RogueLibs.CreateCustomName(CButtonText.Teach_Werewelsh, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Werewelsh",
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
