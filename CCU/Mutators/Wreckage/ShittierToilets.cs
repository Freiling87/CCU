using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.Wreckage
{
	class ShittierToilets
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.ShittierToilets, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Spawns turds around toilets. ESSENTIAL.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.ShittierToilets,
				});
		}
	}
}
