using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Tougher : T_Toughness
	{
		public override int Toughness => 2;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Tougher>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is more willing to enter and stay in combat, like Cannibal, Gorilla & Soldier."),
					[LanguageCode.Spanish] = "Este NPC es aun MAS determinado a entrar y seguir en combate, como un Canibal, Gorila o soldado.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Tougher)),
					[LanguageCode.Spanish] = "Rudisimo",

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
