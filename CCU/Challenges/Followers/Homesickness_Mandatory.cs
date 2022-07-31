using RogueLibsCore;

namespace CCU.Challenges.Followers
{
    class Homesickness_Mandatory : C_Followers
	{
		[RLSetup]
		static void Start()
		{
			PostProcess = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(Homesickness_Mandatory), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers will stay behind when the level is completed, overriding Homesickness Killer. Homesickness Killer is removed from the trait choice pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Homesickness_Mandatory)),
				});
		}
	}
}
