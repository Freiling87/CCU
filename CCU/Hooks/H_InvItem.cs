﻿using BepInEx.Logging;
using CCU.Localization;
using RogueLibsCore;

namespace CCU.Hooks
{
	public class H_InvItem : HookBase<InvItem>
	{
		// Instance = host InvItem

		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public bool vanillaLongerRapidFire;
		public bool vanillaRapidFire;
		public bool initialSetupComplete = false;
		public int vanillaItemValue;

		protected override void Initialize()
		{
			if (!initialSetupComplete)
			{
				vanillaLongerRapidFire = Instance.longerRapidFire;
				vanillaRapidFire = Instance.rapidFire;
				vanillaItemValue = Instance.itemValue;

				initialSetupComplete = true;
			}
		}

		public void AddWeaponMod(string weaponMod)
		{
			Instance.contents.Add(weaponMod);

			switch (weaponMod)
			{
				case vItem.AmmoCapacityMod:
					Instance.maxAmmo = (int)(Instance.maxAmmo * 1.4f);
					break;
				case vItem.RubberBulletsMod:
					Instance.Categories.Add(VItemCategory.NonViolent);
					Instance.Categories.Add(VItemCategory.NotRealWeapons);
					break;

			}
		}
	}

	// HookFactoryBase automatically creates and attaches the hook on object instantiation.
	public class InvItemHookFactory : HookFactoryBase<InvItem>
	{
		public override bool TryCreate(InvItem instance, out IHook<InvItem> hook)
		{
			hook = new H_InvItem();
			return true;
		}
	}
}