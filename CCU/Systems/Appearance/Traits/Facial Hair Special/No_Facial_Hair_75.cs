using CCU.Traits.App_FH1;
using RogueLibsCore;

namespace CCU.Traits.App_FH3
{
	public class No_Facial_Hair_75 : T_FacialHair
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<No_Facial_Hair_75>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Rolls \"No Facial Hair\" 75% of the time, regardless of the number of other items in the pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(No_Facial_Hair_75), "No Facial Hair 75%"),
					[LanguageCode.Spanish] = "Sin Vello Facial 75%",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
