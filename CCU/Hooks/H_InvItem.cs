using BepInEx.Logging;
using RogueLibsCore;

namespace CCU
{
	public class H_InvItem : HookBase<InvItem>
	{
		// Instance = host InvItem

		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public bool initialSetupComplete = false;

		public int vanillaItemValue;
		public bool vanillaLongerRapidFire;
		public bool vanillaRapidFire;

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

		public void TryModWeapon(string weaponMod)
		{
			Agent agent = Instance.agent;
			logger.LogDebug("AddWeaponMod: " + weaponMod);

			if (Instance.contents.Contains(weaponMod))
			{
				logger.LogDebug("Aborted TryModWeapon: Item already has mod");
				return;
			}

			Instance.contents.Add(weaponMod);

			//	TODO: Eliminate this. For now, CombineItems doesn't work reliably.
			switch (weaponMod)
			{
				case VItemName.AmmoCapacityMod:
					Instance.maxAmmo = (int)(Instance.maxAmmo * 1.4f);
					Instance.itemValue = (int)(Instance.itemValue * 1.4f);
					break;
				case VItemName.RubberBulletsMod:
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