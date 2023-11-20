using RogueLibsCore;

namespace CCU.Mutators
{
	public class Continue2 : M_CCU
	{
		public Continue2() : base() { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Continue2())
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Upon death, the player can continue the game from the beginning of the floor. Infinite continues. Starts after you complete Floor 1.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Continue!",
				});
		}
	}
}