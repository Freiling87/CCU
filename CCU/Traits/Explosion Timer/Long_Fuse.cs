using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
	public class Long_Fuse : T_ExplosionTimer
	{
		public override float TimeFactor => 2.00f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Long_Fuse>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format($"Multiplies explosion timer by 2. Multiplier compounds with other Fuse traits. Vanilla fuse is 1.5 seconds."),
					[LanguageCode.Spanish] = "Multiplica el tiempo que este NPC tarda en explotar por 2, Normalmente es 1.5 segundos.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Long_Fuse)),
					[LanguageCode.Spanish] = "Mecha Larga",
				})
				.WithUnlock(new TraitUnlock_CCU
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