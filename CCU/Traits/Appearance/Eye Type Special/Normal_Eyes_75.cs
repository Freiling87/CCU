using CCU.Traits.App_ET1;
using RogueLibsCore;

namespace CCU.Traits.App_ET3
{
	public class Normal_Eyes_75 : T_EyeType
	{
		public override string[] Rolls => new string[] { };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Normal_Eyes_75>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"Normal Eyes\" 75% of the time, regardless of the number of other items in the pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Normal_Eyes_75), "Normal Eyes 75%"),
                    [LanguageCode.Spanish] = "Ojos Normales 75%",
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
