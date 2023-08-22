using BepInEx.Logging;
using CCU.Traits;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Systems.Campaign_Branching
{
	internal class T_Switch : T_CCU
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public T_Switch() : base() { }

		public string Gate;
		public bool Status;
		public bool Frozen;
	}

	internal class AgentSwitchFactory : HookFactoryBase<Trait>
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		internal static readonly List<string> gateNames = new List<string>() { "A", "B", "C", "D" };

		public override bool TryCreate(Trait instance, out IHook<Trait> hook)
		{
			if (instance.traitName.StartsWith("TraitGate_"))
			{
				hook = new T_Switch()
				{
					Gate = instance.traitName[instance.traitName.Length - 1].ToString(),
				};
				return true;
			}

			hook = null;
			return false;
		}

		[RLSetup]
		static void Start()
		{
			RogueFramework.TraitFactories.Add(new AgentSwitchFactory());

			foreach (string levelGate in gateNames)
			{
				string traitName = "Agent_Switch_" + levelGate;

				RogueLibs.CreateCustomName(traitName, NameTypes.StatusEffect, new CustomNameInfo
				{
					[LanguageCode.English] = "Gate " + levelGate,
				});
				RogueLibs.CreateCustomName(traitName, NameTypes.Description, new CustomNameInfo
				{
					[LanguageCode.English] = "Attaches Agent's switches to Gate " + levelGate ,
				});
				RogueLibs.CreateCustomUnlock(new TraitUnlock(traitName)
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
					Upgrade = null,
				});
			}
		}
	}
}