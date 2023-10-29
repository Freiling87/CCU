using RogueLibsCore;

namespace CCU.Traits.Senses.Vision
{
	internal class Eight_Beamed : T_Senses
	{
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Eight_Beamed>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Cop Bot vision beam.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Eight_Beamed)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
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