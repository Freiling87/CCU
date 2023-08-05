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
					[LanguageCode.English] = "Followers act as if their employer had Homesickness Killer. Homesickness Killer is removed from the trait choice pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Homesickness_Disabled)),
				});
		}
	}
}