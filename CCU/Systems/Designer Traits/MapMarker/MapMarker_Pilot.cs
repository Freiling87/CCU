using RogueLibsCore;

namespace CCU.Traits.Map_Marker
{
	public class MapMarker_Pilot : T_MapMarker
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<MapMarker_Pilot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character should spawn with a Map Marker. Let's see what happens!",
					[LanguageCode.Spanish] = "Racism Code Enabler",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(MapMarker_Pilot)),
					[LanguageCode.Spanish] = "E_Sex",

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
