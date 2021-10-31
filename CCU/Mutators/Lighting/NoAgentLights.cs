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
	public static class NoAgentLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NoAgentLights, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agents no longer create light around themselves.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.NoAgentLights,
				});
		}
	}
}
