using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Loyal : T_Rel_Player
    {
        public override string Relationship => VRelationship.Loyal;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Player_Loyal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Loyal to Players.",
                    [LanguageCode.Spanish] = "Este NPC es Leal al jugador.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Player_Loyal)),
                    [LanguageCode.Spanish] = "Leal al Jugador",

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
