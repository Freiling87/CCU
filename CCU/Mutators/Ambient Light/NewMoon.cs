using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace CCU.Mutators.AmbientLight
{
	public static class NewMoon
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.NewMoon, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "No ambient lighting at all. Darkness is pitch dark.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.NewMoon,
				});
		}
	}
}
