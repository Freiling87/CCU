using RogueLibsCore;

namespace CCU.Traits.Bodyguarded
{
    public class Bodyguarded_Pilot : T_Bodyguarded
	{
		public override int BodyguardCountBase => 2;
		public override string BodyguardType => VanillaAgents.Goon;

        //[RLSetup]
        public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Bodyguarded_Pilot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character should spawn with bodyguards. Let's see what happens!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bodyguarded_Pilot)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
