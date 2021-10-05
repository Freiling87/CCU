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
			CustomName hireSafecrack = RogueLibs.CreateCustomName(CJob.SafecrackSafe, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Safecrack",
				[LanguageCode.Russian] = "",
			});
			CustomName hireTamper = RogueLibs.CreateCustomName(CJob.TamperSomething, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Tamper",
				[LanguageCode.Russian] = "",
			});
		}
	}
}
