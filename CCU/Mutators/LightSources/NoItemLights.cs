using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.LightSources
{
	public static class NoItemLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NoItemLights, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Items no longer create light around themselves.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.NoItemLights,
				});
		}
	}
}
