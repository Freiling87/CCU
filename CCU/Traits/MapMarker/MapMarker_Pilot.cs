using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;

namespace CCU.Traits.Map_Marker
{
	public class MapMarker_Pilot : CustomTrait
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<MapMarker_Pilot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character should spawn with a Map Marker. Let's see what happens!",
					[LanguageCode.Russian] = "",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.MapMarker_Pilot,
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
