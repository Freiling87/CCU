using RogueLibsCore;

namespace CCU.Mutators
{
	public class Insert_Nuggets : M_CCU
	{
		public Insert_Nuggets() : base() { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Insert_Nuggets())
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Upon death, the player can continue the game from the beginning of the floor. 25 nuggets per continue. Starts after you complete Floor 1.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Insert_Nuggets)),
				});
		}
	}
}
