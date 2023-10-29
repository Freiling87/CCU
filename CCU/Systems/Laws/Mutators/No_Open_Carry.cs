using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;
using UnityEngine;

namespace CCU.Mutators.Laws
{
	public class No_Open_Carry : M_Law
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public override bool RollInDailyRun => true;
		public override bool ShowInLevelMutatorList => true;

		public No_Open_Carry() : base(nameof(No_Open_Carry), true) { }

		//[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new No_Open_Carry())
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Brandishing a weapon in public is a minor crime. Equip your fists or a non-weapon to keep the cops off your back.",
					[LanguageCode.Spanish] = "Tener tu pistolita afuera es un crimen indecente. Manten tus manos limpias o lleva un arma blanca para que la poli te ignore.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(No_Open_Carry)),
					[LanguageCode.Spanish] = "Porte Abierto Prohibido",
				});
		}

		private static List<string> PublicWeaponsOkay = new List<string>()
		{
			VItemName.BaseballBat,
			VItemName.Fist,
			VItemName.Crowbar,
			VItemName.Wrench,
			VItemName.Sledgehammer,
			VItemName.PoliceBaton,
			VItemName.FireExtinguisher,
			VItemName.Leafblower,
			VItemName.OilContainer,
			VItemName.Taser,
			VItemName.WaterPistol,
			VItemName.ResearchGun,
			VItemName.BananaPeel,
			VItemName.Rock,
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