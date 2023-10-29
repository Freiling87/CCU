using RogueLibsCore;

namespace CCU.Traits.App_HS1
{
	public class Robot_Player_Head : T_Hairstyle
	{
		public override string[] Rolls => new string[] { "RobotPlayerHead" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Robot_Player_Head>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este no-peinado a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Robot_Player_Head)),
					[LanguageCode.Spanish] = "Cabeza de Robot Jugable",
				})
				.WithUnlock(new TU_DesignerUnlock
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