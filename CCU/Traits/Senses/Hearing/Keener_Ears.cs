using RogueLibsCore;

namespace CCU.Traits.Senses
{
	public class Keener_Ears : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Keener_Ears>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Reacts to sound like Bartender, Bouncer, Cannibal & Goon.",
                    [LanguageCode.Spanish] = "Este NPC Reacciona al sonido como un Cantinero, Seguridad, Canibal o Maton.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Keener_Ears)),
                    [LanguageCode.Spanish] = "Oídos Más Agudos",
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
            agent.modVigilant = 2;
        }
    }
}
