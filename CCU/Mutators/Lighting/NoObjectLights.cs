using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.Lighting
{
	class NoObjectLights
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NoObjectLights, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.NoObjectLights,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Objects no longer create light around themselves.",
				});
		}
	}
}
