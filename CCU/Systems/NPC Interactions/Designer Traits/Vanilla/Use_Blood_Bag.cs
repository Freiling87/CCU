using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Use_Blood_Bag : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.UseBloodBag;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& interactingAgent.inventory.HasItem(VItemName.BloodBag);

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Use_Blood_Bag>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can help the player use a Blood Bag in their inventory."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Use_Blood_Bag)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}