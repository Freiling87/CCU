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
                [LanguageCode.Spanish] = "Asistime",
            }); 
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Assist Me",
                [LanguageCode.Spanish] = "Asistime",
            });
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Permanent, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
                [LanguageCode.Spanish] = "Contratar Permanentemente",
            });
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Permanent, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
                [LanguageCode.Spanish] = "Contratar Permanentemente",
            });
			RogueLibs.CreateCustomName(CButtonText.Hire_Expert_Permanent_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
                [LanguageCode.Spanish] = "Contratar Permanentemente",
            });
			RogueLibs.CreateCustomName(CButtonText.Hire_Muscle_Permanent_Voucher, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Hire Permanently",
                [LanguageCode.Spanish] = "Contratar Permanentemente",
            });

			//	Jobs
			t = NameTypes.Interface;
			RogueLibs.CreateCustomName(CJob.DisarmTrap, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Disarm Trap",
                [LanguageCode.Spanish] = "Desarmar Trampa",
            });
			RogueLibs.CreateCustomName(CJob.Pickpocket, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Pickpocket",
                [LanguageCode.Spanish] = "Carterear",
            });
			RogueLibs.CreateCustomName(CJob.Poison, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Poison",
                [LanguageCode.Spanish] = "Envenenar",
            });
			RogueLibs.CreateCustomName(CJob.SafecrackSafe, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Safecrack",
                [LanguageCode.Spanish] = "Forzar Caja Fuerte",
            });
			RogueLibs.CreateCustomName(CJob.TamperSomething, t, new CustomNameInfo
			{
				[LanguageCode.English] = "Tamper",
                [LanguageCode.Spanish] = "Manipular Máquina",
            });
		}
	}
}
