using BepInEx.Logging;
using CCU.Localization;
using CCU.Systems.Object_Variables;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Systems.Containers
{
    class Containers
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<string> ContainerObjects_Slot1 = new List<string>()
		{
			vObject.Barbecue,
			vObject.Bathtub,
			vObject.Bed,
			// vObject.Crate,	Likely has special rules that will need attention
			vObject.Desk,
			vObject.Fireplace,
			vObject.FlamingBarrel,
			vObject.GasVent,	// Require screwdriver
			//vObject.Manhole,	// Need SORCE's code here
			vObject.Plant,
			// vObject.Podium,	Investigateable
			vObject.PoolTable,
			vObject.Refrigerator,
			vObject.Shelf,
			//vObject.SlimeBarrel,	Poison looter
			vObject.Stove,
			vObject.Toilet,
			vObject.TrashCan, 
			vObject.Tube,
			vObject.VendorCart,
			vObject.WaterPump,
			vObject.Well,
		};
		public static List<string> FireParticleEffectObjects = new List<string>()
		{
			vObject.Barbecue,
			vObject.Fireplace,
			vObject.FlamingBarrel,
		};

		public enum ContainerValues
        {
			Hidden,
			// Desk is only lockable of the above. These are mostly pointless.
			//Hidden_Locked,
			//Hidden_Locked_Keycoded,
			//Hidden_Keycoded,
			//Locked,
			//Locked_Keycoded,
			//Keycoded,
			None
        }

		public static string MagicObjectName(string originalName)
		{
			if (IsContainer(originalName))
				return vObject.ChestBasic;

			return originalName;
		}

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.OpenContainer, t, new CustomNameInfo("Search"));
			RogueLibs.CreateCustomName(CButtonText.Ransack, t, new CustomNameInfo("Ransack"));
			RogueLibs.CreateCustomName(COperatingBarText.Ransacking, t, new CustomNameInfo("Ransacking"));

			t = NameTypes.Dialogue;
			RogueLibs.CreateCustomName("StashHint_Barbecue_01", t, new CustomNameInfo("I won't make it for Christmas this year, sorry. You won't end up with a lump of coal, at least. But you'll probably have to dig through some to find it. Hope you were good this year!"));
			RogueLibs.CreateCustomName("StashHint_Barbecue_02", t, new CustomNameInfo("The cops ransacked the place again this week, and they looked hungry for evidence. Well, unless they're hungry for some pork ribs they're not gonna find what they're looking for."));
			RogueLibs.CreateCustomName("StashHint_Bathtub_01", t, new CustomNameInfo("That thing we stole... it's too hot to sell yet, even to a fence. Let's stay out of prison for now. I hid it somewhere where it's safe to drop the soap."));
			//etc., aim for 3 each.
			// Dialogue is spoken on item pickup, shown in an Investigation window if item activated

			RogueLibs.CreateCustomName(CDialogue.CantAccessContainer_TooHot, t, new CustomNameInfo("It's too hot to touch!"));
			RogueLibs.CreateCustomName(CDialogue.CantAccessContainer_ManholeClosed, t, new CustomNameInfo("I need a crowbar."));
			RogueLibs.CreateCustomName(CDialogue.CantAccessContainer_TubeFunctional, t, new CustomNameInfo("It's still running, and I want to keep all my limbs."));


			RogueInteractions.CreateProvider(h => 
			{
				if (IsContainer(h.Object.objectName) && !h.Helper.interactingFar)
				{
					Agent agent = h.Object.interactingAgent;

					if (h.HasButton(VButtonText.Open))
						h.RemoveButton(VButtonText.Open);

					if (!h.Object.objectInvDatabase?.isEmpty() ?? false)
                    {
						if (h.Object is VendorCart)
							h.AddButton(CButtonText.Ransack, m =>
							{
								if (!m.Agent.statusEffects.hasTrait(VanillaTraits.SneakyFingers))
								{
									GC.audioHandler.Play(m.Object, VanillaAudio.Operating);
									GC.spawnerMain.SpawnNoise(m.Object.tr.position, 0.4f, m.Agent, "Normal", m.Agent);
									GC.OwnCheck(m.Agent, m.Object.go, "Normal", 2);
								}

								m.StartOperating(2f, true, COperatingBarText.Ransacking);
							});
						else
							h.AddImplicitButton(CButtonText.OpenContainer, m =>
							{
								TryOpenChest(m.Object, agent);
							});
					}
				}
			});
		}

		public static void TryOpenChest(PlayfieldObject playfieldObject, Agent agent)
		{
			bool isHot =
				(FireParticleEffectObjects.Contains(playfieldObject.objectName) && playfieldObject.ora.hasParticleEffect) ||
				(playfieldObject is FlameGrate flameGrate && !(flameGrate.myFire is null));
			bool grabHotStuff =
				agent.HasTrait(VanillaTraits.FireproofSkin) ||
				agent.HasTrait(VanillaTraits.FireproofSkin2) ||
				agent.statusEffects.hasStatusEffect(VStatusEffect.ResistFire);

			if (isHot && !grabHotStuff)
			{
				agent.SayDialogue(CDialogue.CantAccessContainer_TooHot);
				return;
			}
			else if (playfieldObject is Manhole manhole && !manhole.opened)
			{
				agent.SayDialogue(CDialogue.CantAccessContainer_ManholeClosed);
				return;
			}
			else if (playfieldObject is Tube tube && tube.functional)
			{
				agent.SayDialogue(CDialogue.CantAccessContainer_TubeFunctional);
				return;
			}

			playfieldObject.ShowChest();
		}

		// Expanded later
		public static bool IsContainer(string objectName) =>
			!(objectName is null) &&
			ContainerObjects_Slot1.Contains(objectName);
	}
}