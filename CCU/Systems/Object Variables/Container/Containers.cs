using BepInEx.Logging;
using CCU.Localization;
using RogueLibsCore;
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