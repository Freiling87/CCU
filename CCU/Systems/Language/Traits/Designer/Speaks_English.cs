using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_English : T_Language
	{
		public Speaks_English() : base() { }

		public override string[] VanillaSpeakers => new string[] { };
		public override string[] LanguageNames => new string[] { LanguageSystem.English };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_English>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This is a back-end trait. You shouldn't see it."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_English)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { VanillaTraits.VocallyChallenged },
					IsAvailable = false,
					IsAvailableInCC = false,
					UnlockCost = 0,
					//Unlock = { upgrade = nameof(Polyglot) }
					Unlock =
					{
						categories = { VTraitCategory.Social },
					}
				});
		}
	}
}