using RogueLibsCore;

namespace CCU.Traits.App_AC1
{
	public class Fedora : T_Accessory
	{
		public override string[] Rolls => new string[] { "Fedora" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Fedora>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool. For those who studied the blade.",
					[LanguageCode.Spanish] = "Agrega este accesorio a los que el personaje puede usar, si eres un tremendo friki por supuesto",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Fedora)),
					[LanguageCode.Spanish] = "Fedora",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
