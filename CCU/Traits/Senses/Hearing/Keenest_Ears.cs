using RogueLibsCore;

namespace CCU.Traits.Senses
{
	public class Keenest_Ears : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Keenest_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Reacts to sound like Supercop.",
                    [LanguageCode.Spanish] = "Este NPC Reacciona al sonido como un Superpolicía",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Keenest_Ears)),
                    [LanguageCode.Spanish] = "Oídos Aun Más Agudos",
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

        public void SetupAgentStats(Agent agent)
        {
            agent.modVigilant = 3;
        }
    }
}
