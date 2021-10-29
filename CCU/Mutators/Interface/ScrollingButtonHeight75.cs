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
	class ScrollingButtonHeight75
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.ScrollingButtonHeight75, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.ScrollingButtonHeight75,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Resizes Scrolling Buttons to 75% of their original height.",
				});
		}
	}
}
