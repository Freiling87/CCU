using RogueLibsCore;

namespace CCU.Challenges.Progression
{
	class Big_Quest_Mandatory : C_Progression
	{
		//[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(Big_Quest_Mandatory), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If you fail your Big Quest, you fuckin' explode. YOU FUCKIN' EXPLODE!!!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Big_Quest_Mandatory)),
				});
		}
	}
}