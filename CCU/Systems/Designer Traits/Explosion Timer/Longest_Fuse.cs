using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
	public class Longest_Fuse : T_ExplosionTimer
	{
		public override float TimeFactor => 4.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Longest_Fuse>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format($"Multiplies explosion timer by 4. Multiplier compounds with other Fuse traits. Vanilla fuse is 1.5 seconds."),
					[LanguageCode.Spanish] = "Multiplica el tiempo que este NPC tarda en explotar por 4, Normalmente es 1.5 segundos.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Longest_Fuse)),
					[LanguageCode.Spanish] = "Mecha Prolongada",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}