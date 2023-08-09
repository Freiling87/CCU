using CCU.Traits.App;
using RogueLibsCore;

namespace CCU.Traits.App_SS3
{
	public class Legless : T_Appearance, ISetupAgentStats
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Legless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Not an elf, dammit!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Legless)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() 
		{
			Owner.agentHitboxScript.legsColor = new UnityEngine.Color32(0, 0, 0, 0);
		}
		public override void OnRemoved() 
		{
			// Too bad sucka
		}
		public void SetupAgentStats(Agent agent)
		{
			Owner.agentHitboxScript.legsColor = new UnityEngine.Color32(0, 0, 0, 0);
		}
	}
}
