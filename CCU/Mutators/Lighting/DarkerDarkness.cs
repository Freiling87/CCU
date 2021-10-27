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
	class DarkerDarkness
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.DarkerDarkness, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.DarkerDarkness,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Ambient lighting is reduced.",
				});
		}
	}
}
