using CCU.Challenges;
using RogueLibsCore;

namespace CCU.Mutators.Interface
{
	internal class No_Maps : C_CCU
	{
		public override bool RollInDailyRun => true;
		  
		//[RLSetup]
		static void Start()
		{
			PostProcess = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(No_Maps), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "No minimap (and effectively, no teleport).",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(No_Maps)),
				});
		}
	}
}