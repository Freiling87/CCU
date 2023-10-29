using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	public class FunnyPack_Pro : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<FunnyPack_Pro>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent can spawn with unlimited Pocket items.\n\n" +
					"<i>You're a serious work-doing type of person-inspired humanoid worker product. You have good important NEEDS for a big fannypack to keep all your shit in. This will be the key to your success. Get <b>FunnyPack Pro©</b> right now today quickly!</i>",
					[LanguageCode.Spanish] = "NPC puede spawnear con una cantidad ilimitada de items de bollsillo addicionales.\n\n" +
					"<i>-Si llama ente los proximos 20 minutos le daremos el Riñon Madre para proteger todos tus bienes en el espacio ilimitado de tus organos internos, Compra <b>Riñones©</b> hoy antes que se terminen!</i>",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(FunnyPack_Pro)),
					[LanguageCode.Spanish] = "Super Riñonero Extremo",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
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