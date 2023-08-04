using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
    public class Untrusting : T_InteractionGate
    {
        public override int MinimumRelationship => 3;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Untrusting>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will only interact with Friendly or better.\n\n" +
                    "Exceptions: \n" +
                    "- Leave Weapons Behind\n" +
                    "- Offer Motivation\n" +
                    "- Pay Debt"),
                    [LanguageCode.Spanish] = String.Format("Este NPC solo interactura con quienes sean Amistosos o mejor.\n\n" +
                    "Excepciónes: \n" +
                    "- Dejar Armas\n" +
                    "- Ofrecer Motivacion\n" +
                    "- Pagar Deuda"),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Untrusting)),
                    [LanguageCode.Spanish] = "Desconfiado",

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
