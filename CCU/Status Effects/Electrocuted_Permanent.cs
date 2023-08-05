using BepInEx.Logging;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Status_Effects
{
	[EffectParameters(EffectLimitations.RemoveOnDeath | EffectLimitations.RemoveOnKnockOut)]
	public class Electrocuted_Permanent : CustomEffect
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		internal static GameController GC => GameController.gameController;

		[RLSetup]
		internal static void Setup()
		{
			RogueLibs.CreateCustomEffect<Electrocuted_Permanent>()
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Electrocuted (Permanent)",
				})
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Getting zapped, but with infinite Z's",
				});
		}

		public override int GetEffectHate() => 5;
		public override int GetEffectTime() => 9999;
		public override void OnAdded()
		{
			Owner.electrocuted = true;
			GC.spawnerMain.SpawnStateIndicator(Owner, VanillaEffects.Electrocuted);
			Owner.statusEffects.CantDoAnything(VanillaEffects.Electrocuted);
			Owner.Say("", true);
			Owner.agentHitboxScript.MustRefresh();
			Owner.agentHitboxScript.UpdateAnim();
		}
		public override void OnRemoved()
		{
			Owner.electrocuted = false;

			if (Owner.statusEffects.electrocutionParticles != null)
				Owner.statusEffects.electrocutionParticles.GetComponent<ParticleSystem>().Stop();
			
			GC.spawnerMain.SpawnStateIndicator(Owner, "");
			
			if (Owner.isPlayer > 0 && Owner.localPlayer)
				GC.playerControl.SetCantPressGameplayButtons(VanillaEffects.Electrocuted, 0, Owner.isPlayer - 1);
			
			GC.tileInfo.DirtyWalls();
			Owner.SetRunBackToPosition(true);
			
			if (Owner.lastHitByAgent != null && Owner.movement.HasLOSObject360(Owner.lastHitByAgent))
			{
				Owner.SetJustHitByAgent(Owner.lastHitByAgent);
				Owner.attackCooldown = 2f;
			}
			
			Owner.agentHitboxScript.MustRefresh();
			Owner.agentHitboxScript.UpdateAnim();
		}
		public override void OnUpdated(EffectUpdatedArgs e) { }
	}
}
