using RogueLibsCore;

namespace CCU.Traits.Behavior
{
	public class Vigilantest : T_Behavior
	{
		public override void SetupAgent(Agent agent)
		{
			agent.modVigilant = 3;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Vigilantest>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Reacts to sound like Supercop.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vigilantest)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
	}
}