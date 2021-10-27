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
	class TrashierTrashcans
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.TrashierTrashcans, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.TrashierTrashcans,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Spawns litter around Trashcans.",
				});
		}
	}
}
