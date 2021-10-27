using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.Followers
{
	class HomesicknessMandatory
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.HomesicknessMandatory, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.HomesicknessMandatory,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers always stay behind when you leave a level.",
				});
		}
	}
}
