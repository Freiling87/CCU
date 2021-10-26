using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU
{
	public static class CCUNames
	{
		[RLSetup]
		public static void InitializeNames()
		{
			CustomName jobDisarmTrap = RogueLibs.CreateCustomName(CJob.DisarmTrap, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Disarm Trap",
				[LanguageCode.Russian] = "",
			});
			CustomName jobPickpocket = RogueLibs.CreateCustomName(CJob.Pickpocket, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Pickpocket",
				[LanguageCode.Russian] = "",
			});
			CustomName jobPoison = RogueLibs.CreateCustomName(CJob.Poison, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Poison",
				[LanguageCode.Russian] = "",
			});
			CustomName jobSafecrack = RogueLibs.CreateCustomName(CJob.SafecrackSafe, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Safecrack",
				[LanguageCode.Russian] = "",
			});
			CustomName jobTamper = RogueLibs.CreateCustomName(CJob.TamperSomething, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Tamper",
				[LanguageCode.Russian] = "",
			});
		}
	}
}
