using BepInEx.Logging;
using CCU.Localization;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Systems.Containers
{
    class Containers
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<string> ContainerObjects = new List<string>()
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
			vObject.Shelf,
			//vObject.SlimeBarrel,	Poison looter
			vObject.Stove,
			vObject.Toilet,
			vObject.Tube,
			vObject.VendorCart,
			vObject.WaterPump,
			vObject.Well,
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

		// TODO: verify which data field you're using for this
		public static bool IsStash(ObjectReal objectReal) =>
			throw new NotImplementedException();
			//objectReal.extraVarString.Contains("Hidden");

		public static string MagicObjectName(string originalName)
		{
			if (ContainerObjects.Contains(originalName))
				return vObject.ChestBasic;

			return originalName;
		}

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.OpenContainer, t, new CustomNameInfo("Search"));

			t = NameTypes.Dialogue;
			RogueLibs.CreateCustomName("StashHint_Barbecue_01", t, new CustomNameInfo("If I don't get back into town for Christmas, don't worry. You won't end up with a lump of coal, but you'll probably have to dig through some to find it. Hope you were good this year!"));
			RogueLibs.CreateCustomName("StashHint_Barbecue_02", t, new CustomNameInfo("The cops ransacked the place again this week, and they looked hungry for evidence. Well, unless they're hungry for some pork ribs they're not gonna find what they're looking for."));
			RogueLibs.CreateCustomName("StashHint_Bathtub_01", t, new CustomNameInfo("That thing we stole... it's too hot to sell yet, even to a fence. Let's stay out of prison for now. I hid it somewhere where it's safe to drop the soap."));
			//etc., aim for 3 each.
			// Dialogue is spoken on item pickup, shown in an Investigation window if item activated

			RogueInteractions.CreateProvider(h => 
			{
				if (ContainerObjects.Contains(h.Object.objectName))
				{
						Agent agent = h.Object.interactingAgent;
						bool grabHotStuff =
							agent.HasTrait(VanillaTraits.FireproofSkin) ||
							agent.HasTrait(VanillaTraits.FireproofSkin2) ||
							agent.statusEffects.hasStatusEffect(VStatusEffect.ResistFire);

						// Curse of Legibility
						if ((!grabHotStuff && 
							(
								(h.Object.ora.hasParticleEffect &&
								(
									h.Object is Barbecue ||
									h.Object is Fireplace ||
									h.Object is FlamingBarrel)
								) ||
								(h.Object is FlameGrate flameGrate && !(flameGrate.myFire is null))
							)) ||
							(h.Object is Manhole manhole && !manhole.opened) ||
							(h.Object is Tube tube && tube.functional))
							return;

						if (!h.Object.objectInvDatabase?.isEmpty() ?? false)
							h.AddImplicitButton(CButtonText.OpenContainer, m =>
							{
								m.Object.ShowChest();
							});
				}
			});
		}
	}
}