using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.Utility
{
	class SortMutatorsByName
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.SortMutatorsByName, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Sorts active Mutators by name."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.SortMutatorsByName,
				});
		}
	}
}
