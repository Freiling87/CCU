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
	public static class NoObjectLights
	{
		[RLSetup]
		public static void Start()
		{
		UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NoObjectLights, true))
			.WithDescription(new CustomNameInfo
			{
				[LanguageCode.English] = "Objects no longer create light around themselves.",
			})
			.WithName(new CustomNameInfo
			{
				[LanguageCode.English] = CMutators.NoObjectLights,
			});
		}
	}
}
