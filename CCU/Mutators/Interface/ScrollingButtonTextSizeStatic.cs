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
	class ScrollingButtonTextSizeStatic
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.ScrollingButtonTextSizeStatic, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.ScrollingButtonTextSizeStatic,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables text-resizing in Scrolling Button menus.",
				});
		}
	}
}
