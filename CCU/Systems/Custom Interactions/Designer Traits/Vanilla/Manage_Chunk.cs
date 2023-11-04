using CCU.Traits.Loadout_Chunk_Items;
using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Manage_Chunk : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => "";
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Manage_Chunk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will do Clerk/Jock behaviors if they're placed in certain chunks:\n" +
					"- Arena\n" +
					"- Deportation Center\n" +
					"- Hotel *\n\n" +
					"*<color=red>Requires</color>: {0}", DocumentationName(typeof(Chunk_Key))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Manage_Chunk)),

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
		
		
	}
}
