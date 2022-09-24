﻿using RogueLibsCore;

namespace CCU.Traits.App_BT1
{
	public class Bouncer_Body : T_BodyType
    {
		public override string[] Rolls => new string[] { VanillaAgents.Bouncer };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Bouncer_Body>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bouncer_Body)),
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
