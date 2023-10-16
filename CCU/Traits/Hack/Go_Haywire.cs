using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Hack
{
	public class Go_Haywire : T_Hack
	{
		public override string ButtonText => VButtonText.Hack_Haywire;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Go_Haywire>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be hacked to go Haywire.\n\n" +
					"<color=red>Requires:</color> Electronic"),
					[LanguageCode.Spanish] = "Este NPC se puede hackear para enloquecer.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Go_Haywire)),

				})
				.WithUnlock(new TraitUnlock_CCU
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