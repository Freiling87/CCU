using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.Utility
{
    public class SortTraitsByName : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<SortTraitsByName>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sorts all currently added traits by Name, Ascending.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.Utility_SortTraitsByName,
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() 
        {
            List<Trait> traits = new List<Trait>();

            foreach (Trait trait in Owner.statusEffects.TraitList)
			{
                if (!(trait is SortTraitsByName))
                    traits.Add(trait);
                Owner.statusEffects.RemoveTrait(trait.traitName);
			}

            traits.Sort();

            foreach(Trait trait in traits)
                Owner.statusEffects.AddTrait(trait.traitName);
        }
        public override void OnRemoved() { }
    }
}
