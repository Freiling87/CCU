using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Common_Folk : T_TraitGate
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Common_Folk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This NPC will be Loyal to a player with Friend of the Common Folk."),
					[LanguageCode.Spanish] = "Este NPC es leal a cualquier personaje que tenga el rasgo Amigo de la Gente Común.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Common_Folk)),
					[LanguageCode.Spanish] = "Amigo Común",
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}