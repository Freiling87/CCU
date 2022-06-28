using RogueLibsCore;

namespace CCU.Challenges.Followers
{
    class Homesickness_Disabled : C_Followers
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(Homesickness_Disabled), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers act as if their employer had Homesickness Killer.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DisplayName(typeof(Homesickness_Disabled)),
				});
		}
	}
}
