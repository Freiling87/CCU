using CCU.Traits.App_ET1;
using RogueLibsCore;

namespace CCU.Traits.App_ET3
{
	public class Normal_Eyes_50 : T_EyeType
	{
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Normal_Eyes_50>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"Normal Eyes\" 50% of the time, regardless of the number of other items in the pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_Eyes_50), "Normal Eyes 50%"),
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
