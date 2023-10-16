using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Mech_Pilot_Body : T_BodyType
	{
		public override string[] Rolls => new string[] { VanillaAgents.MechPilot };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Mech_Pilot_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mech_Pilot_Body)),
					[LanguageCode.Spanish] = "Cuerpo de Piloto de Mecarobot",
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