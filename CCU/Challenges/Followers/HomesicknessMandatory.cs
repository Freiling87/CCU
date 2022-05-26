using RogueLibsCore;

namespace CCU.Challenges.Followers
{
    class HomesicknessMandatory
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(CMutators.HomesicknessMandatory, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers always stay behind when you leave a level.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CMutators.HomesicknessMandatory,
				});
		}
	}
}
