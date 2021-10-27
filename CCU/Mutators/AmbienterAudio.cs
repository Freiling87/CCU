using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators
{
	class AmbienterAmbience
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.AmbienterAmbience, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.AmbienterAmbience,
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Enables some previously un-used ambient audio, particularly for Casinos and Graveyards.",
				});
		}
	}
}
