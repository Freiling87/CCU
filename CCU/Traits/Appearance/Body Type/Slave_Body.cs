﻿using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Slave_Body : T_BodyType
    {
		public override string[] Rolls => new string[] { VanillaAgents.Slave };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Slave_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
                    [LanguageCode.Spanish] = "Agrega este cuerpo a los que el personaje puede usar.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Slave_Body)),
                    [LanguageCode.Spanish] = "Cuerpo de Esclavos",
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
