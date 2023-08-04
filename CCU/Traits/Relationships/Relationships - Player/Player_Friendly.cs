using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Friendly : T_Rel_Player
    {
        public override string Relationship => VRelationship.Friendly;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Player_Friendly>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Friendly to Players.",
                    [LanguageCode.Spanish] = "Este NPC es Amistoso al Jugador.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Player_Friendly)),
                    [LanguageCode.Spanish] = "Amistoso al Jugador",

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
