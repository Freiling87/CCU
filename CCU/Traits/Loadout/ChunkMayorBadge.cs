using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;

namespace CCU.Traits.Loadout
{
	public class ChunkMayorBadge : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<ChunkMayorBadge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("When placed in a Mayor's Office chunk, this character will by default hold the Visitor's Badge. If multiple characters have this trait, one will be chosen randomly.\n\n<color=orange>Recommended:</color> {0}", CTrait.Interaction_MayorClerk),
					[LanguageCode.Russian] = "",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.Loadout_ChunkMayorBadge,
					[LanguageCode.Russian] = "",
				})
				.WithUnlock(new TraitUnlock
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
