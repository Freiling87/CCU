using CCU.Traits.App_BT1;
using RogueLibsCore;

namespace CCU.Traits.App_BT2
{
	public class Killer_Robot_Body_Greyscale : T_BodyType
	{
		public override string[] Rolls => new string[] { "G_" + VanillaAgents.KillerRobot };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Killer_Robot_Body_Greyscale>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Killer_Robot_Body_Greyscale)),
					[LanguageCode.Spanish] = "Cuerpo de Robot Asesino Gris",
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
