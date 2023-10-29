using RogueLibsCore;


// Distribute and eliminate this file


internal static class CJob
{
	public const string
		DisarmTrap = "[CCU] Job - DisarmTrap",
		Pickpocket = "[CCU] Job - Pickpocket",
		Poison = "[CCU] Job - Poison",
		SafecrackSafe = "[CCU] Job - SafecrackSafe",
		SafecrackSafeReal = "[CCU] Job - SafecrackSafeReal",
		TamperSomething = "[CCU] Job - TamperSomething",
		TamperSomethingReal = "[CCU] Job - TamperSomethingReal";
}
internal static class CTraitCategory
{
	public const string
		Tampering = "Tampering",
		Unarmed = "Unarmed";
}
internal static class COperatingBarText
{
	public const string
		Ransacking = "Ransacking";
}

internal static class CDetermineMoneyCost
{
	public const string
		HirePermanentExpert = "HackerAssist_Permanent",
		HirePermanentMuscle = "SoldierHire_Permanent",
		LearnLanguageEnglish = "LearnLanguageEnglish",
		LearnLanguageOther = "LearnLanguageOther";
}
internal static class CButtonText
{
	// CButtonText
	public const string
		//	Agent Interactions
		Hire_Expert_Permanent = "AssistMe_Permanent",
		Hire_Expert_Permanent_Voucher = "AssistMe_Permanent_Voucher",
		Hire_Expert_Voucher = "AssistMe_Voucher", // Name differentiation required to avoid button confusion in RL
		Hire_Muscle_Permanent = "HireAsProtection_Permanent",
		Hire_Muscle_Permanent_Voucher = "HireAsProtection_Permanent_Voucher",
		Hire_Muscle_Voucher = "HireAsProtection_Voucher", // Name differentiation required to avoid button confusion in RL

		//	Object Interactions
		Container_Open = "ContainerOpen",
		Hack_Explode = "Hack_Explode",
		Investigate = "Investigate",
		Ransack = "Ransack";
}

internal static class CCUNames
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
