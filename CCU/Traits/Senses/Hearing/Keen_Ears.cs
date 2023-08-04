using RogueLibsCore;

namespace CCU.Traits.Senses
{
	public class Keen_Ears : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Keen_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Reacts to sound like Shopkeeper, Slavemaster & Soldier.",
                    [LanguageCode.Spanish] = "Este NPC Reacciona al sonido como un Tendero, Amo de los Esclavos o Soldado.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Keen_Ears)),
                    [LanguageCode.Spanish] = "Oído Agudo",
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
            agent.modVigilant = 1;
        }
    }
}
