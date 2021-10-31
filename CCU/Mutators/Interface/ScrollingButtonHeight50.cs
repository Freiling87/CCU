using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.Interface
{
	class ScrollingButtonHeight50
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.ScrollingButtonHeight50, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Resizes Scrolling Buttons to 50% of their original height.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.ScrollingButtonHeight50
				});
		}
	}
}
