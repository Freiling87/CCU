<p align="left">
<img width = "160" src="../Images/CCU_Large.png" alt="CCU Logo" align="left" style="image-rendering: -moz-crisp-edges; image-rendering: -o-crisp-edges; image-rendering: -webkit-optimize-contrast; image-rendering: pixelated; -ms-interpolation-mode: nearest-neighbor;">
<img width = "160" src="../Images/CCU_Large.png" alt="Yeah there are two, so what" align="right" style="image-rendering: -moz-crisp-edges; image-rendering: -o-crisp-edges; image-rendering: -webkit-optimize-contrast; image-rendering: pixelated; -ms-interpolation-mode: nearest-neighbor;">
</p>

<h1 align="center">
<br>
Change Log
</h1>
<br><br>

##		v 1.0.0
 
- **Feature additions**
  - Agent Default Goals
    - Arrested
    - Burned
    - Dead
    - Gibbed
    - Knocked Out
  - Mutators
    - Followers
      - Homesickness Disabled[^1]
      - Homesickness Mandatory[^1]
  - Objects: New extra variables have been added. Check the Objects feature page linked from the main page of the readme for specifics on the categories below.
    - Containers: Various objects can now store ONE item as a container.
    - Investigateables: In the editor, you can now add text to certain objects and it will be readable as if it were a Sign. 
      - WARNING: This is NOT for reading emails. That would be rude!
  - Traits
    - Behavior
      - Grab Alcohol
      - Grab Everything
      - Grab Food
    - Cost Scale
      - Much More (200% cost)
    - Drug Warrior Modifier
      - Suppress Syringe A/V
    - Explode On Death
      - Dizzy
      - EMP
      - Firebomb
      - Noise Only
      - Slime
      - Stomp
      - Water
    - Gib Type
      - Ectoplasm
      - Gibless
      - Glass Shards
      - Golemite
      - Ice Shards
      - Leaves
      - Meat Chunks
    - Language: For use with or without Vocally Challenged. Characters without Vocally Challenged are assumed to speak English (sorry, it's canon).
      - Speaks Binary[^1]
      - Speaks Chthonic[^1]
      - Speaks ErSdtAdt[^1]
      - Speaks Foreign[^1]
      - Speaks High Goryllian[^1]
      - Speaks Werewelsh[^1]
    - Passive
      - Blinker[^1]
      - Brainless
      - Immovable
      - Indestructible
      - Not Vincible
    - Relationships - Faction
      - Faction Firefighter Aligned
      - Faction Gorilla Aligned 
- **Tweaks**
  - Loadout - Mayor Badge is deactivated for the time being. There's a lot of hardcoded logic for the Mayor Village and I need to be more thorough before I can release related features.
  - Pay Debt is now scaled to Cost Scale traits
  - Untrusting/er/est: Added exceptions for Leave Weapons Behind, Offer Motivation, Pay Debt & Pay Entrance Fee.
- **Bugfixes**
  - Pick Pockets no longer cancels the setup of various interaction and merchant traits
  - Drug Warrior - Wildcard now runs without crashing the game
  - Text color restored to yellow for non-English versions (this was a feature test accidentally left in)
  - Vending Machines' cost interactions now load correctly
  - Codpiece now spawns correctly in shops
  - Influence Election no longer persists after use
  - Removed Research Gun from Tech Mart & Research inventories
  - Chunk Key Holder & Chunk Safe Combo Holder now actually work
  - Honorable Thief now correctly gates Shop Access vis-a-vis Honor Among Thieves
  - CCU traits now correctly hidden from Augmentation Booth, Possession Trait List
  - Decoupled various Killer Robot behaviors that were hardcoded to Seek & Destroy (Water damage, EMP vulnerability, Knockback bonus, walking through Doors). 
- **Feature Limitation**
  - Bad news: Continuing a saved run will not load all mod content. This is way beyond my technical ability to fix, and not scoped in Roguelibs at this point. So unfortunately, this is not likely to be fixed in the near future. Mod content should be played in a single session for best results.
- **Trait Update System:** I've renamed and slightly reorganized some of the traits. This system should automatically update outdated traits both on spawn and on loading in the character editor. You will not have to update character files, and all versions of CCU will be backwards-compatible with un-updated content.
  - **Class Name Overlaps:** A few traits shared names with certain vanilla classes, causing their description in the character select page to be overwritten.
    - Hire Type
      - Hacker → Cyber-Intruder
    - Merchant Type
      - Shopkeeper → General Store
      - Soldier → Army Quartermaster
      - Thief → Intruder's Warehouse
      - Vampire → Bloodsucker Bazaar
  - **Class-based Faction Relationship Traits:** Most of the agent-based relationship traits only included hostility to a class' enemies. This doesn't fully cover the scope of the vanilla feature, so these have been expanded to treat certain agent types as factions. The traits now include mutual alignment with vanilla agents of that class, plus mutual hostility with that class' enemies.
    - Bashable → Faction Blahd Aligned
    - Crushable → Faction Crepe Aligned
    - Hostile to Cannibal → Faction Soldier Aligned
    - Hostile to Soldier → Faction Cannibal Aligned
    - Specistist → Faction Gorilla Aligned

[^1]: This is a Player feature. This means it will be accessible by players, and won't have all the [CCU] name prefixes that Designer-only content has. It also costs Nuggets to unlock, because CCU is a scam to get your precious nuggets. Sucker!