using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using UnityEngine;

namespace CCU.Mutators.Laws
{
    internal class No_Open_Carry : C_Law
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[RLSetup]
		static void Start()
		{
			PostProcess = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(No_Open_Carry), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Brandishing a weapon in public is a minor crime. Equip your fists or a non-weapon to keep the cops off your back.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(No_Open_Carry)),
				});
		}

		private static List<string> PublicWeaponsOkay = new List<string>()
		{
			vItem.BaseballBat,
			vItem.Fist,
			vItem.Crowbar,
			vItem.Wrench,
			vItem.Sledgehammer,
			vItem.PoliceBaton,
			vItem.FireExtinguisher,
			vItem.Leafblower,
			vItem.OilContainer,
			vItem.Taser,
			vItem.WaterPistol,
			vItem.ResearchGun,
			vItem.BananaPeel,
			vItem.Rock,
		};

		public override bool IsViolating(Agent agent)
		{
			Vector3 pos = agent.curPosition;

			// Disallow cop-owned floors
			// Allow non-cop-owned floors

			if (!agent.enforcer &&
				!GC.tileInfo.IsIndoors(pos) &&
				!PublicWeaponsOkay.Contains(agent.inventory.equippedWeapon.invItemName))
				return true;

			return false;
		}
		public override string LawText => "Misdemeanor: Open carry of a weapon in a public space.";
		public override int Strikes => 1;
		public override string[] WarningMessage => new[] { "Put that away!" };
    }
}