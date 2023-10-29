using CCU.Traits.Explosion_Modifier;
using RogueLibsCore;

namespace CCU.Traits.Explosion_Timer
{
	public class Short_Fuse : T_ExplosionTimer
	{
		public override float TimeFactor => 0.66f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Short_Fuse>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = string.Format($"Multiplies explosion timer by 0.66. Multiplier compounds with other Fuse traits. Vanilla fuse is 1.5 seconds."),
					[LanguageCode.Spanish] = "Multiplica el tiempo que este NPC tarda en explotar por 0.66, Normalmente es 1.5 segundos.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Short_Fuse)),
					[LanguageCode.Spanish] = "Mecha Corta",
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