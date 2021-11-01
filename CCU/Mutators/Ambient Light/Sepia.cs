using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.AmbientLight
{
	public static class Sepia
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.Sepia, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Washed out and yellowed, like an old photograph.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.Sepia,
				});
		}
	}
}
