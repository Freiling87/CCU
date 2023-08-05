using BepInEx.Logging;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Status_Effects
{
	[EffectParameters(EffectLimitations.RemoveOnDeath | EffectLimitations.RemoveOnKnockOut)]
	public class Frozen_Permanent : CustomEffect
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		internal static GameController GC => GameController.gameController;

		[RLSetup]
		internal static void Setup()
		{
			RogueLibs.CreateCustomEffect<Frozen_Permanent>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Frozen (Permanent)",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "All frozed up and for good this time.",
				});
		}

		public override int GetEffectHate() => 5;
		public override int GetEffectTime() => 9999;
		public override void OnAdded()
		{
			Owner.frozen = true;
			Owner.objectSprite.frozen = true;
			Owner.objectSprite.agentColorDirty = true;
			Owner.statusEffects.CantDoAnything(VanillaEffects.Frozen);
			GC.spawnerMain.SpawnStateIndicator(Owner, "NoAnim");
			Owner.Say("", true);
		}
		public override void OnRemoved()
		{
			if (!Owner.dead)
				Owner.frozen = false;

			if (Owner.isPlayer > 0 && Owner.localPlayer)
				GC.playerControl.SetCantPressGameplayButtons(VanillaEffects.Frozen, 0, Owner.isPlayer - 1);

			Owner.objectSprite.frozen = false;
			Owner.objectSprite.agentColorDirty = true;
			Owner.objectSprite.SetRenderer("Off");
			GC.tileInfo.DirtyWalls();
		}
		public override void OnUpdated(EffectUpdatedArgs e) { }
	}
}
