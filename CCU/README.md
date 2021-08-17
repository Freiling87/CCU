# Streets of Rogue Custom Content Utilities

N.B.: Bold text indicates completed/functional features. Any feature names not bolded are planned but not yet implemented.

## Core
- Player/Creator editions
  - Player edition hides traits in creation menus, simply to keep them un-cluttered

## Appearances

Effects should be self-explanatory. Let me know if any are unclear.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Appearance: Hair Color - Normal					|
|Appearance: Hair Color - Normal, No Grey			|
|Appearance: Hair Color - Wild						|

And so on. There will be traits for skin color, hair color, hair style, and facial hair.

## Behaviors
  - Increase Trait slots by 1, since it will not affect player character

Effects should be self-explanatory. Let me know if any are unclear.

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|AI: Bartender BuyRound								|
|AI: Bartender Vendor
|AI: Bouncer Bribeable
|AI: Bouncer GuardDoor
|AI: ButlerBot Clean
|AI: Cannibal Cannibalize
|AI: Cannibal AmbushBush
|AI: Cannibal AmbushManhole
|AI: Cannibal HostileToSoldiers
|AI: Clerk Bank
|AI: Clerk BloodBank
|AI: Clerk DeportationCenter
|AI: Clerk Hotel
|AI: Clerk MovieTheater
|AI: Cop AcceptBribe
|AI: Cop EnforceLaws
|AI: Cop Lockdown
|AI: CopBot Enforcelaws
|AI: CopBot VisionBeams
|AI: Doctor AdministerBloodBag
|AI: Doctor Heal
|AI: Doctor UseBloodBag
|AI: DrugDealer UseDrugs
|AI: DrugDealer Vendor
|AI: Firefighter FightFire
|AI: Gangbanger SpawnRoamingGangs
|AI: Gorilla Hire
|AI: Gorilla Vendor
|AI: Hacker Hack
|AI: Jock ArenaManager
|AI: KillerRobot Chase
|AI: Mayor Bodyguarded SuperCop
|AI: Mobster InfluenceElection
|AI: Mobster Shakedown
|AI: Musician Bodyguarded Goo
|AI: Musician Bodyguarded Supergoo
|AI: Musician Turntables
|AI: OfficeDrone OfferMotivation
|AI: Scientist Identify
|AI: Shapeshifter Possess
|AI: Shopkeeper Vendor
|AI: Soldier HostileToCannibals
|AI: Soldier MallVendor
|AI: Slave Enslaved
|AI: Slavemaster SellSlaves
|AI: Slavemaster OwnSlaves
|AI: SlumDweller CauseRuckus
|AI: SlumDweller CommonFolk
|AI: SlumDweller GrabMoney
|AI: Supercop EnforceLaws
|AI: Thief AmbushManhole
|AI: Thief BreakIn
|AI: Thief HonorAmongThieves
|AI: Thief Pickpocket
|AI: UpperCruster Bodyguarded
|AI: UpperCruster OwnSlave
|AI: UpperCruster Tattle
|AI: Vampire Bite
|AI: Vampire HostileToWerewolves
|AI: Various AnnoyedAtSuspicious
|AI: Various Coward
|AI: Various Hire
|AI: Various Extort
|AI: Various Guilty
|AI: Various Scumbag

## Faction Traits

- Treats ownership as shared (hopefully possible)

|Trait								|Effect													|
|:----------------------------------|:------------------------------------------------------|
|Faction 1 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 1 Hostile					|NPC hostile to all NPCs with Faction 1 Aligned			|
|Faction 2 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 2 Hostile					|NPC hostile to all NPCs with Faction 2 Aligned			|
|Faction 3 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 3 Hostile					|NPC hostile to all NPCs with Faction 3 Aligned			|
|Faction 4 Aligned					|NPC aligned with all NPCs with same trait				|
|Faction 4 Hostile					|NPC hostile to all NPCs with Faction 4 Aligned			|

## Level Editor

|Input								|Function												|
|:----------------------------------|:------------------------------------------------------|
|Arrow Keys							|Set Object/NPC placement direction						|
|Q, E								|Rotate Object/NPC placement direction					|
|**[1 - 9]**		     			|Switch to Layer (Wall, Floor, Floor2, etc.)			|
|Ctrl + [1 - 9]						|Switch to Layer and open Type Selector					|
|Ctrl + Z							|Undo													|
|Ctrl + Y							|Redo													|
|**Ctrl + S** 						|Save current file										|
|**Ctrl + O**						|Open file												|
|**Ctrl + A**						|Select all in Layer									|
|Tab								|Move through input fields								|

## Others
- Spawn objects placed regardless of district (currently limited to district-appropriate objects)
