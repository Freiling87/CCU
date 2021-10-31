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
	class LitterallyTheWorst
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.LitterallyTheWorst, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Spawns trash in exterior areas.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.LitterallyTheWorst,
				});
		}
	}
}
