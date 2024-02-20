using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Systems.Interaction
{
	//	Why was this a Player Trait? And shouldn't this be in RHR? 
	//	An NPC version might be called "Generous" or "Easy Mark"
	internal class Panhandler : T_PlayerTrait
	{
		private const string
			BegMoney = "BegMoney";

		[RLSetup]
		private static void AddInteraction()
		{
			RogueInteractions.CreateProvider<Agent>(h =>
			{
				Agent agent = h.Object;
				Agent interactingAgent = h.Agent;
				InvItem money = agent.inventory.FindItem(VanillaItems.Money);
				int moneyCount = money?.invItemCount ?? 0;

				if (!interactingAgent.HasTrait<Panhandler>()
					|| money is null || moneyCount == 0)
					return;


				// TODO: Custom FindThreat method
				h.AddButton(BegMoney, $"${money.invItemCount} ({agent.relationships.FindThreat(interactingAgent, false)}%)", m =>
				{
					int myChance = agent.relationships.FindThreat(interactingAgent, true); // TODO: Custom FindThreat

					if (GC.percentChance(myChance) || agent.relationships.GetRel(interactingAgent) == VRelationship.Aligned || agent.relationships.GetRel(interactingAgent) == VRelationship.Submissive)
					{
						agent.inventory.DestroyItem(money);
						interactingAgent.inventory.AddItemAtEmptySlot(money, false, false);
						agent.SayDialogue(VDialogue.NA_GivingQuestItem, true);
						agent.gc.audioHandler.Play(interactingAgent, VanillaAudio.SelectItem);
						return;
					}

					agent.StopInteraction();
					agent.SayDialogue(VDialogue.NA_WontJoinA, true);
					//agent.relationships.SetRel(interactingAgent, VRelationship.Annoyed);
					agent.relationships.SetStrikes(interactingAgent, 2);
					//agent.relationships.SetRelHate(interactingAgent, 5);
				});
			});
		}

		[RLSetup]
		private static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Panhandler>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You can beg people for money. Failed attempts will annoy them."
					// TODO: Supercops don't allow panhandling
					// TODO: + version: They may give a free item and always become friendly
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Panhandler)),
				})
				.WithUnlock(new TU_DesignerUnlock());

			RogueLibs.CreateCustomName(BegMoney, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Beg",
			});
		}
	}
}