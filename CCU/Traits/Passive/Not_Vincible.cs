using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	// N.B.: Named because adding a trait with the same name as a status effect will just give you the status effect. Code you can smell before you even see it!
	public class Not_Vincible : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Not_Vincible>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Go ahead, try to vince them. They simply can't be vinced."),
					[LanguageCode.Spanish] = "Este NPC no puede ser vencido por un ser tan debil. Nota: jugar con este personaje puede causar bugs extraños no esperados.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Not_Vincible)),
					[LanguageCode.Spanish] = "No Vencible",
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