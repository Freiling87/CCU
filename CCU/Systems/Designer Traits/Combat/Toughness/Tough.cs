using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Tough : T_Toughness
	{
		public override int Toughness => 1;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Tough>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is more willing to enter and stay in combat, like Bartender, Blahd/Crepe & Firefighter."),
					[LanguageCode.Spanish] = "Este NPC esta mas determinado a entrar y seguir en combate, como un Cartinero, Gangster o Bombero.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Tough)),
					[LanguageCode.Spanish] = "Rudo",

				})
				.WithUnlock(new TU_DesignerUnlock
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
