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
	class ScrollingButtonTextAlignLeft
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.ScrollingButtonTextAlignLeft, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Left-Aligns text in Scrolling Button menus.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.ScrollingButtonTextAlignLeft,
				});
		}
	}
}
