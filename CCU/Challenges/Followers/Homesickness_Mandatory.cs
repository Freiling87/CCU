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
					[LanguageCode.English] = "Followers will stay behind when the level is completed, overriding Homesickness Killer.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DisplayName(typeof(Homesickness_Mandatory)),
				});
		}
	}
}
