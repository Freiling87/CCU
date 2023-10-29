using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
	public class Speedbagger : T_PlayerTrait, IRefreshAtEndOfLevelStart
	{
		public bool BypassUnlockChecks => false;
		public void Refresh() { }
		public void Refresh(Agent agent)
		{
			agent.agentInvDatabase.fist.rapidFire = true;
		}
		public bool RunThisLevel() => true;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speedbagger>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Your fists have Rapid Fire.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speedbagger)),
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 15,
					Unlock =
					{
						categories = { CTraitCategory.Unarmed },
					}
				});
		}

		public override void OnAdded()
		{
			Owner.agentInvDatabase.fist.rapidFire = true;
		}
		public override void OnRemoved()
		{
			Owner.agentInvDatabase.fist.rapidFire = false;
		}
	}
}