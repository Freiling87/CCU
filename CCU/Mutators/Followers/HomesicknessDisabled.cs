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
	class HomesicknessDisabled
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.HomesicknessDisabled, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers never stay behind when you leave a level.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.HomesicknessDisabled,
				});
		}
	}
}
