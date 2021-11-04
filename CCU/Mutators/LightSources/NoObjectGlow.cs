using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.LightSources
{
	public static class NoObjectGlow
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NoObjectGlow, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Objects no longer flash yellow if you have items you can use on them.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.NoObjectGlow,
				});
		}
	}
}
