#			How to read this
If you wandered in here out of curiosity, this is my working notes file, and completed/planned feature list. It's a markdown file, but best viewed in raw form since a lot of its organization has to do with text characters' alignment.

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features
#		Scope
##			C	Thanks
Forgot to add thanks notes to documentation, don't forget it again.
##			P	Bug Fixing
###				C	Hole bug
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	SORCE.Patches.P_PlayfieldObject.P_Hole.Hole_EnterRange (UnityEngine.GameObject myObject, Hole __instance) (at <1f7534e775f047b78adf6c12ea42e7b0>:0)
	Hole.EnterRange (UnityEngine.GameObject myObject) (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	Hole.OnTriggerStay2D (UnityEngine.Collider2D other) (at <9086a7372c854d5a8678e46a74a50fc1>:0)
##			P	0.1.1 Changelog
- **Feature additions**
  - Mutators
    - Followers
      - Homesickness Disabled
      - Homesickness Mandatory
  - Traits
    - Behavior
      - Grab Alcohol
      - Grab Everything
      - Grab Food
    - Cost Scale
      - Much More (200% cost)
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
    - Language (For use with or without Vocally Challenged)
      - ErSdtAdt Speaker
      - Foreign Speaker
      - Lang Zonbi Speaker
      - High Goryllian Speaker
      - Werewelsh Speaker
    - Passive
      - Indestructible
      - Not Vincible
    - Relationships - Faction
      - Faction Firefighter Aligned
      - Faction Gorilla Aligned 
  - Objects
    - It's a banner day for the rude... you can now read emails! In the editor, you can now add text to certain objects and it will be readable as if it were a Sign. 
- **Tweaks**
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
- **Bug Acknowledgement**
  - Saved runs may not load mod content correctly. This is a limitation of RogueLibs and beyond my technical ability to implement (or even understand, to be honest). So for the time being, there's no roadmap to resolve this.
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
###			C	Flex Traits
Enable existing traits to player side and make their display name conditional on whether the mod is in Player or Designer mode. However, it doesn't fit neatly into a dichotomy - designers might still want to play, and they should have the same experience as player edition users. There needs to be a list of "Flex Traits" or some better name for this special category, since it will have unique rules for when to display the names in certain formats.
##			T	Test note
20220725
##			T	Container/Ivestigateable interaction
InvDatabase.FillChest ~923 uses component.extraVarString, check if NameDB found rather than just null
Attempt: P_ObjectMultObject.OnDeserialize_Postfix
If that doesn't work, Use Magic Strings for ExtraVarStrings lower down in the same method.
##		CT	Drug Warrior Modifiers
GoalBattle.Process is where the effect is applied.
###			T	Suppress Syringe AV
The `-Syringe` text is just clutter
The sound is sometimes not applicable lorewise
	P_GoalBattle.Process_GateSyringeAV
###			T	Extended Release
Duration:
	P_GoalBattle.CustomStatusDuration(Agent agent)
Deactivation:
	P_GoalBattle.Terminate_Postfix
###			T	Eternal Release
Test
P_GoalBattle.CustomStatusDuration(Agent agent)
###			C	Last Stander
Effect triggers when they would flee instead of at beginning of combat
####			C	Extended Release interaction
When paired with ER, the effect lasts until they would no longer be intimidated. 
###			C	Post Warrior
Effect triggers on end of threat (Regenerate, smoke, invisible)
###			C	Whatta Rush
Effect gains 1s of duration on take/receive damage
##		CT	Explode On Death
###			T	Custom Explosion System
New
###			T	Oil Spill
Explosion.SetupExplosion ~373
###			T	Do they explode when... exploded?
Verify by kill w rocket
###			C	Explodes when Arrested
New
###			√	Cop Bot not Exploding
Complete
###			√	Certain explosion types don't delete body
P_StatusEffects_ExplodeBody.DisappearBody
###			√	Gib body
P_StatusEffects_ExplodeBody.GibItAShot
##		C	Gib Type
###			C	Robot Gibs
New
###			C	Wood
For use with Leaves, once they can combine into one
###			√	Ectoplasm
Complete
###			√	Gibless (No gibs)
Complete
###			√	Glass Shards
Complete
###			√	Ice Shards
Complete
###			√	Leaves
Complete
###			√	Meat Chunks
Complete
##		CT	Passive
###			C	Blinker
Blink to a random nearby spot when hit
This is valid for player characters, so might need to be another mod
###			C	Concealed Carrier
Hides weapon until drawn
###			C	Explodevice Eligible
agent.canExplosiveStimulate
###			C	Holographic
Ghostlike, not necessarily gibbable (Use Supernatural for that)
###			C	Immobile
Can't move (for turret types)
###			C	Invincible
New
###			C	Mute Dialogue
Cancels possible immersion-breaking dialogue tailored to vanilla NPCs
###			C	Oblivious
Doesn't care about destroyed property, dead teammates, or noises in restricted areas. 
But will enter combat if their teammates do.
###			C	Psychic Shield
Wiki: "The Alien is unaffected by mind altering items such as a Hypnotizer or Haterator, but can be affected by items like Rage Poison or the Satellite's Happy Waves."
###			C	Reviveable (Infinite)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it unlimited times.
###			C	Reviveable (One)
Instead of dying, agent will be Injured instead. Player can revive them or hire someone to do it once.
###			C	Reviver
If hired and surviving, will revive the player once
###			C	Spidey-Sensitive
When alerted, immediately enters combat with perp (no search necessary)
###			C	Statue
Remove colors
Tint white
Make stationary, Invincible, non-reactive
###			C	Stinky Aura
Werewolf A-were-ness works on this character
###			C	Supernatural
Ghost Gibber works
###			C	Tattletale
Reports ALL crimes to alarm button
###			C	Unappetizing
Can't be bitten/cannibalized
###			C	Translucent
Ghost visual effect
###			C	Unchallenging
No XP for neutralization
###			C	Vision Beams (Cop Bot)
DW
###			C	Zombified
Agent.zombified
Agent.customZombified
###			√	Berserk (Declined)
Rel General - Hostile to All
###			√	Crusty
Complete
###			√	Extortable
Complete
###			C!	Guilty
####			C!	Cascade to Employees
SetRelationshipOriginal, under Drug Dealer
###			√	Indestructible
Complete
###			√	Innocent
Complete
###			√	Possessed
Complete
###			√	Status Effect Immune
Complete
###			√	Un-Vincible
Complete
###			√	Z-Infected
Complete
##		CT	Goals
###			CT	Actual Goals
####			T	Commit Arson
####			C	Flee Danger
DW (What's the point anyway)
####			C	Robot Clean
DW
###			C	Scene Setters
####			C	Arrested
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	AgentInteractions.ArrestAgent (Agent agent) (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	CCU.Systems.CustomGoals.CustomGoals.RunSceneSetters (Agent agent) (at <8ec24f2cb37249d98375c99ba6268ebe>:0)
	CCU.Patches.Level.P_LoadLevel.SetupMore5_Postfix (LoadLevel __instance) (at <8ec24f2cb37249d98375c99ba6268ebe>:0)
	LoadLevel.SetupMore5 () (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	LoadLevel+<SetupMore4_2>d__150.MoveNext () (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
####			C	Burned
DW
####			C	Dead
Works but gibs. No gib.
####			√	Gibbed
Complete
####			√	Knocked Out
Complete
##		T	Language - Designer
###			T	ErSdtAdt Speaker
###			T	Foreign Speaker
###			T	High Goryllian Speaker
###			T	Lang Zombi Speaker
###			T	Werewelsh Speaker
##		C	Language - Player
###			C	Mute
No interactions under any circumstances, even with Translator
###			C	Polyglot
Gain 2 languages on trait gain, and an additional one every two levels.
###			C	Telepathic (rename)
Can talk to all languages, even Mutes
#	CT	Projects
##			C	Language (Overhaul)
###			C	00 Mutator: Language System
- NPCs have a chance to speak a foreign language
  - If a shopkeeper or bartender speaks a second language, generate a Neon Sign in front of their business in that language (we can dream, right?)
- NPCs have a chance to have Vocally Challenged
  - If they do, they always have at least one foreign language
- Some people become Friendly if you interact in a non-English language. Chance is higher if they don't speak English.
- Enables Polyglot trait choice
- Gives the Translator an actual reason to exist
- All hired agents can act as translators
- Every District has a set of Our Town mutators (below) that may trigger on level 2 of the district.
###			C	00 Non-Disaster Group: Our Town
Gated behind Language System Mutator
Shifts the population so that about 66% are speakers, and one third - 1 half of those don't speak English.
Or you know what, make an overhaul mutator mapped to each class. That's what this is turning into.
- Concrete Jungle (Monkese)
- District 69 (ErSdtAdt)
- Little Faraway (Foreign)
- Meltingpot District (Even distribution of all language groups)
- Werewales (Werewelsh)
- Brainard (Lang Zonbi)
###			C	00 Trait: Polyglot
Trait choice locked behind Language System Mutator
- Gain 2 languages on taking the trait
- Gain 1 langauge every 3 levels
###			C	Trait: ____ Speaker
These will be the first Flex Traits (see other section) for CCU.
###			√	00 Base Feature
P_Agent.
	CanUnderstandEachOther_Postfix
###			√	Alienian
###			√	Monkese
###			√	Zombese
##			CT	Legacy Name Updater
###				C	Iterate until failure
When you have multiple layers of patches, names may undergo more than one permutation. Iterate the name-changing method until failure.
###				T!	Challenges
Homesickness Mandatory & Disabled
####				T	Test Mutator List in editor
Had to tweak it
###				√	Traits
####				√	Designer Side
P_Unlocks.GetUnlock_Prefix
####				√	Player Side 
P_StatusEffects.AddTrait_Prefix
##			H	Config Files
###				Custom Flag list
Allow player to name booleans uniquely.
These should be grouped by Campaign, since that's the only place they're valid.
The config file should match the name of the campaign, if they allow the same characters.
###				Custom Level Tag List?
Not so sure about the utility of this. I don't think players should need more than 4 level tags.
- Whenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.
##			C	Relationship Refactor
###				C	Create SetRelationship(agent, otherAgent, VRelationship)
Current state is embarrassing 
##			C	Explosion Trait Refactor
Current Setup:
	Explode on Death
Proposal:
	Event Fuse
		Short
		Long
		None
	Event Type
		Explosion
	Event Trigger
		Align
		Combat
		Damage Dealt
		Damage Taken
		Death
		Exit Level
		Join Party
		Search
		Spawn
##			C	Dedicated section on Character Sheet
Should not be too hard, as the one method where it's filled out is pretty transparent
Just add a --- CCU TRAITS --- Divider or something
##			H	Config Files
###				Custom Flag list
Allow player to name booleans uniquely.
These should be grouped by Campaign, since that's the only place they're valid.
The config file should match the name of the campaign, if they allow the same characters.
###				Custom Level Tag List?
Not so sure about the utility of this. I don't think players should need more than 4 level tags.
- Whenever you have enough in the campaign to make it playable, test it in Player Edition and see if the experience is the same.
##			C	Door System
###				C	Keycard System
Red, Blue, Green, Yellow
Work only on Keycard Doors and Keycard Safes
####				C	Access Scope
Determines how far the keycard will work. Doesn't have to use every tier of this, just being expansive.
	Chunk
	Chunk Type
	Level
	District
	City
	Mayor Village
	All
####				C	Access Level
Configurable in Keycard and Keycard Door
####				C	RFID Flipping
If someone is holding a Keycard, you can grab their card's RFID signal to add it to your keyring. 
This can be done in the manner of a normal hack, or with a Signal Grabber passive inventory item - no interaction required, but shorter range. Good tool for the undercover.
####				C	Keycard Doors
#####					C	Hack
- Admit Owners*
- Admit Non-Owners
- Admit No One
- Admit All
- Something Mischievous
- Trigger Alarm
- Turn Off/On
#####					C	Tamper
- Disconnect (a la Security Camera)
#####					C	Automatic Door option
This would be neato
###				C	Key Ring System
Keys are specific to an object. One door, one chest, etc. so chunks may generate with multiple. 
Desk is a lockable container.
####				C	Keyring
Keyring stores all keys in one slot
##			C	Stash System
Variable field in editor, maybe with some basic setting variations (destructible, Investigateable, etc.)
Locks access to the object as a Chest unless the player holds the matching Stash Hint.
You don't know the object holds an item until you find the Stash Hint item somewhere. This could be in an Agent's inventory, or hidden elsewhere in the chunk. 
##			T	Legacy Updater
###				T!	Challenges
Homesickness Mandatory & Disabled
####				T	Test Mutator List in editor
Had to tweak it
###				√	Trait
####				√	Designer Side
P_Unlocks.GetUnlock_Prefix
####				√	Player Side 
P_StatusEffects.AddTrait_Prefix
##		C	Trait Utilities
###			C	Hide Traits in Collapsed Groups
- While in Character Creator, hide traits in Collapsed Groups
  - Once all traits are in they're going to get hard to manage. 
###			C	Sort active Traits by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
- DW
###			C	Sort active Traits by Value
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
#		C	Traits
##		C	Accent Color
Combine w/ Accent Effect traits
##		C	Accent Effect
Apply Accent Color trait to target effect
###			C	Agent Glow
###			C	Nametag (Space/hover)
###			C	Vision Beam
##		C	Agent Group
However this is implemented, there's a danger of subscribed content doing this in an unwanted way. How to control it?
###			C	Slum NPCs (Pilot)
New
###			C	Affect Campaign
Pending pilot
###			C	Affect Vanilla 
Pending pilot
###			C	Roamer Level Feature
New
##		C	Appearance
###			C	Full-randomization bug
- Whole appearance is randomized when any appearance trait is added.
  - Should be a simple fix since it's doing less rather than more.
###			C	Facial Hair
####			C	Trait names changed
Changed trait names to not overlap with vanilla names. 
This will likely break the BEARD MACHINE, because it was based directly on the class names.
####			C	Vanilla facial hair no longer spawns
New
###			C	Hair Type
New
###			C	Skin Color
New
##		C	Behavior
###			C	Absconder
Once hostile to a player, flees to exit elevator
###			C	Ambush
Clearer way to present "Secretly Hostile" relationship, especially since it's not even a relationship
How a Bounty Ambusher is set up:
	agent.bountyHunter
	agent.bountyHunterProjectileWeapon
	Relationships.SetSecretHate
		You should remove this from the Player Relationship algo, since it would interfere with this behavior opaquely
	Relationship.SecretHate
		Note that it's on Relationship (singular), meaning you'll have to find the particular rel
###			C	Clean Trash
New
###			C	Curious
Investigate noises like cop & shopkeeper
###			C	Fucking Salesman
Knocks on non-restricted locked doors, moves on after a pause
###			C	Fight Fires
Agent.firefighter
Agent.fightsFires
###			C	First Aider
Revives Aligned if they can within a certain timer
###			C	Heister
Picks a chest on the level, and tries to fight their way to loot it. 
If successful, deactivates behavior.
###			C	Hobo Beg (Custom)
Maybe just implement the whole Hey, You! overhaul here
###			C	Hog Turntables
New
Allow paid Musician behavior
###			C	La Migra
Should also spawn Deportation Center (See SORCE)
###			C	Mutinous
Agent.mutinous
###			C	Needful
Will seek out Musician need objects and operate them
###			C	Non-Enforcer Enforcer
See if there's any use for Agents who enforce laws but aren't agent.enforcer
###			C	Paranoid
Constant search state
###			C	SecretHate
Agent.secretHate
Agent.choseSecretHate
I think this is Bounty behavior
Didn't work but if you get it working it should be under Behavior - Ambush, since that will make more sense to users.
###			C	Shakedown Player (Mobster)
New
Use this on leader w/ Wander Level
Use "Follow" behavior on agents placed behind them
No need for "Roaming Gang" Trait itself
###			C	Sleepyhead
Will default to finding a bed to sleep in, returning even after combat.
###			C	Slowpoke
Takes a longer time to react to everything
###			C	Stop & Frisk
Should also spawn Confiscation Center (See SORCE)
###			C	Suicdal
Walks into hazards, only those they can see
###			C	Tattle (Upper Cruster)
New
###			C	Arsonist
Arsonist behavior
###			C	Mad Bomber
Place a Time Bomb in a public toilet
###			C	Bio-Terrorist
Poison a random vent or pump
###			C	Wage-Exempt
Will "mug" you for a tip after any transaction
###			√C	Accident-Prone
Works for: Crusher, Fire Spewer, Saw Blade
####			C	Slime, Floor Trigger, ??
New
###			C	Vandal
Destroys public objects or Windows on a whim
###			√	Eat Corpse
Complete
###			√	Grab Alcohol
Complete
###			√	Grab Drugs
Complete
###			√	Grab Everything
Complete
###			√	Grab Food
Complete
###			√	Grab Money
Complete
###			√	Pickpocket
Complete
###			√	Seek & Destroy (Killer Robot)
Complete
####			√	Blinks on Death
Complete
##		C	Campaign Flags
Might need to limit this to a single flag, since having multiple true at the same time would complicate things
###			C	If Paid then Flag A/B/C/D True
New
###			C	If Killed then Flag A/B/C/D True
Etc.
##		H	Combat
###			H	Nth Wind
Refreshes Drug Warrior & Backed Up bools after combat ends.
P_GoalCombatEngage.
	Terminate_Postfix
P_StatusEffects.
	Resurrect	
###			H	Backed Up
P_GoalBattle.
	Process_StartCombatActions
	DoCombatActions
Test: 
	[Error  : Unity Log] AI Update Error: Custom (1124) (Agent)
	Repeated on loop.
IIRC this error comes from a try-catch chain that can be sort of a pain to break open. For that reason I'm shelving this.
###			H	Gloater
Will dance on corpses and make loud noise if they win a combat
This one's kinda hard... Probably on hold for now.
###			H	Lockdowner
Apparently Lockdown walls are broken in custom levels.
###			√	Coward
Complete
###			√	Fearless
Complete
##		C	Cost Currency
###			C	00 Button ExtraCost Display
Bananas & alcohol are hardcoded
To display them correctly, prefix WorldSpaceGUI.ShowObjectButtons (interprets magic numbers)
###			C	Alcohol
A la Bouncer
###			C	Banana
Test
###			C	Blood
Blood Bags always an option
If Vampirism, allow drink
###			C	Flesh
Require Cannibalism? Maybe not
##		C	Drug Warrior
###			C	Extendo-Wheels
Gain Roller Skates
###			C	Suicide Bomber
Initiate a 15s timer, then detonate a Huge explosion
Interface with Timer traits and Explosion traits to allow player to customize
###			C	Sweaty
Gain Wet, lmao
##		C	Explode On Death
###			C	00 Refactor
See other Explosion trait groups
This category will become Explosion Type
###			C	Monke Parasites
Explodes into Monke (barrel style)
###			C	Thoughts & Prayers
Fireworks
###			C	Oil Spill
This doesn't exist but should follow water logic. Plus there are many other uses.
###			C	Ooze
Only did particle effect
###			C	Ridiculous
Only did particle effect, didn't end slow-mo, no kills.
This is the Bomb disaster one, so it will need special attention.
###			C	Slime
Does particle effect and poisons
Do the sprite later.
###			C	Stomp
Did particle effect
Pushed body away
Didn't stun anyone
###			H	00 Does not explode when killed with Cyanide
There's no vanilla precedent for this particular situation, so let's see what users prefer.
###			H	00 Explodes when Arrested
Not too concerned, considering this is vanilla for Slaves.
###			√	Big
Complete
###			√	Dizzy
Complete
###			√	EMP
Complete
###			√	Firebomb
Complete
###			√	Huge
Complete
###			√	Noise Only
Complete
###			√	Normal
Complete
###			√	Warp
Complete
###			√	Water
Complete
##		C	Explosion Timer
Vanilla for EOD is 1.5 seconds
###			C	Cinematic Fuse
Base value 3m, meant to be used with Ridiculous Explosion
Displays as with Disaster
###			C	Suppress Countoff
No countoff numbers
Works with Cinematic Disaster countoff too
###			C	Suppress Red Blink
No red blink before explosion
###			C	Long Fuse
3.00s
5m with Cinematic
###			C	Longer Fuse
5.00s
10m with Cinematic
###			C	Longest Fuse
10.00s
15m with Cinematic
###			C	Short Fuse
0.75s
1m with Cinematic
###			C	Zero Fuse
0.0
##		C	Explosion Timer Trigger
###			C	Combat Start
###			C	Death
###			C	Low Health
###			C	Spawn
This is more of a utility, to allow designers to explode or burn things at level start.
##		C	Hack
###			C	00 Interrupts
Works with Electronic, but hacking bar is interrupted
###			C	Align
New
###			C	Explode
New
###			C	Free Transactions
New
###			C	Give Inventory To Player
New
###			C	Give Single Order
As if they were follower
This includes Expert actions, like Lockpick, etc... but maybe not Hack, since chaining hacks might be a can of worms
###			T	Go Haywire
Attempted
###			C	Increase Odds of Winning
For gambling bots
###			C	Increase Buyer Prices
For Buyer traits
###			C	Join Party
New
###			C	Make Noise
A la arcade machine
###			C	Play Bad Music
Busker?
###			C	Reduce Prices
New
###			C	Security Cam/Turret Options
New
###			C	Security Core
All Security Hack options
###			C	Spit Out Money
A la ATM
###			T	Tamper With Aim
Attempted
###			C	Triangulate
Head towards hacker when interactFar starts
AgentInteractions.HackSomething in CopBot block
###			C	Triangulate for Network

###			C	Unlock Safes & Doors
Maybe set it so they all unlock on death too, or make a separate Unlock All On Death trait.
###			C	Buy Round for Allied
Buy a round for a patron and anyone with the same owner ID in the chunk.
No drink for that guy in the corner. Fuck that guy.
###			C	Buy Slave
Pending actual assignment of owned slaves 
###			C	Clone (Group)
####			C	Clone Player
####			C	Clone Random Character (Separate from Hack function)
####			C	Clone Item
####			C	Clone Target Character
###			H	Cybernetics (Group)
This would be an overhaul
Get Eric's cybernetic traits as a start
####			H	Implant
New
####			H	Remove
Some are simply negatives, acquired or from spawn
####			H	Repair
New
###			C	Deactivation Protocol
Try to hack an Electronic agent in-person via vocal injection attack
###			C	Faction (Group)
####			C	Pay respects to Faction
Costs $1,000 to bump reputation up one level (Hostile → Annoyed etc)
"Improve Faction Relations"
Should only allow for 1 of these to simplify algorithm
But this means you'll need further faction traits
####			C	Sell Intel to Faction 
The idea is, if you were really on their side you wouldn't charge for information. 
Reverse of buying into faction. Just a way to get cash in exchange for slightly reducing your relation. Friendly or better.
###			C	Gamble
####			C	All-In
High risk, high reward
####			C	Bluff
Uses your social skill isntead of luck?
####			C	Cheat
Uses your stealth skill instead of luck
####			C	Play it Close
####			C	Risky Bet
####			C	Safe Bet
####			C	Sore Loser
###			C	Heal (Group)
####			C	All
Like Doctor heal, but all in party with calculated price
####			C	Other
Like Doctor heal, but activates reticle so you can select a party member or other.
####			C	Partial
Like Doctor heal, but at a Blood Bag level.
####			C	Mouseover Price
Show price to heal targeted agent before selection
###			C	Item Storage
A la ATM
"Smuggler?"
###			C	Quest Giver
New
###			C	Real Estate Agent
####			C	Buy Property
($500): Next level, one chunk (Bar, nightclub, Shop, etc.) will have its owners Submissive to you. You gain a $100 passive income that may be reduced if the chunk is damaged.
####			C	Pump & Dump
($250): Boost up a local business, only to pull the rug out from under the suckers! Next level, one chunk (above types) will be Hostile to you. You get a single payment of up to $375, depending on how much the chunk is damaged. Doing this three times will give you Ideological Clash.
####			C	Sell Property
Lose your smallest owned property income. Immediately gain twice its return amount. Rant about taxes.
###			C	Refill/Repair (Group)
####			C	Refill Guns
New
####			C	Repair Armor
New
####			C	Repair Weapons
New
###			C	Sell Mystery Item
A la Goodie Dispenser
###			C	Start Election
New
###			C	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
###			C	Train (Group)
New
####			C	HP Gain
####			C	Stat Gain
New
####			C	Trait Gain
Sell traits for double their Upgrade Machine cost
#####				C	Defense
New
#####				C	Guns
New
#####				C	Melee
New
#####				C	Movement
New
#####				C	Social
New
#####				C	Stealth
New
#####				C	Trade
New
####			C	Trait Removal
New
####			C	Trait Swap
New
####			C	Trait Upgrade
New
###			M	Chunk Visitor's Badge
Set Bribe options on separate traits
Move to Loadout
###			√	Administer Blood Bag
Complete
###			√	Borrow Money
Complete
###			√	Borrow Money (Moocher)
Complete
###			√	Bribe Cops
Complete
###			√	Bribe For Entry (Alcohol)
Complete
###			√	Buy Round
Complete
###			√	Give Blood
Complete
###			√	Heal (Player)
Complete 
###			√	Identify
Complete
###			√	Influence Election
Complete
###			√	Leave Weapons Behind
Complete
###			√	Manage Chunk
####			√	Arena
Complete
####			√	Deportation Center
Complete
####			√	Hotel
Complete
###			√	Offer Motivation
Complete
###			√	Pay Debt
Complete
###			√	Pay Entry Fee
Complete
###			√	Play Bad Music
Complete
###			√	Use Blood Bag
Complete
##		C	Hire Damage Tolerance
###			C	Anyweather
Hiree will leave if a Hostile sees them
###			C	Fairweather
Hiree will leave if they're damaged in combat
"I didn't sign up for this! You're nuts!"
###			C	Foulweather
Hiree will never leave due to damage
##		H	Hire Modifiers
A couple are technically Hire Triggers, so I broadened this cat name
###			C	Homesick 
Automatic Homesickness Killer
###			C	Homesuck
Overrides Homesickness Killer
###			C	Permanent Hire
~8x normal hire price
Allows infinite uses of skills.
When "Not feeling too hot," they'll flee combat but not leave the party, if that's possible.
P_AgentInteractions.
###			C	Permanent Hire Only
As above, but removes the single-use hire option.
###			C	Start as Hired
On level entry
##		C	Hire Trigger
Obv default to On Hire, maybe use a secret default trait to keep the code clean
###			C	On Use Altar
"Activate Servants"
###			C	On Use Podium
"Who's With Me?!"
##		C	Hire Type
###			C	Chloroform
New
###			C	Devour Corpse
New
###			C	Disarm Trap
New
###			C	Drink Blood
New
###			C	Hack
####			C	Cyber Nuke
Giving character Cyber Nuke allows Blow Up option, but it doesn't work
###			C	Handcuff
New
###			C	Mug
One-time use, mug target NPC
###			C	Pickpocket
New
###			C	Poison
New 
###			C	Safecrack
- Reticle does not activate 
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire
	[Info   :  CCU_Core] DetermineButtons_Prefix: Hire Order
	[Info   :  CCU_Core] PressedButton_Prefix: Method Call
	[Debug  :CCU_P_AgentInteractions]       buttonText: [CCU] Job - SafecrackSafe

Here's what comes up for Lockpick job:
	Agent
√		.GetCodeFromJob
√		.GetJobCode					Need to extend jobType enum
√		.ObjectAction
	AgentInteractions
√		.DetermineButtons
√		.LockpickDoor				
√		.PressedButton	
	GoalDoJob
√		.Activate					Check out 
√		.Terminate
	GoalDetails						E_GoalDetails
√		.LockpickDoorReal
	GoalLockpickDoor				GoalSafecrackSafe
√		.Activate
√		.Process
√		.Terminate
	GoalLockpickDoorReal			GoalSafecrackSafeReal
√		.Activate
√		.Process
√		.Terminate
	InvInterface
√		.ShowTarget					
√		.ShowTarget2
	ObjectMult
		.ObjectAction				Not sure yet - are these just logging messages, or are they important?
	PlayfieldObjectInteractions
√		.TargetObject

Objects to Analyze/track:
	Agent
		job
		jobCode
		target.targetType

In P_AgentInteractions.SafecrackSafe, I used JobType.GetSupplies as a placeholder until I figure out how to add to enums.
###			C	Set Explosive
On door or Safe: Plants door detonator
Elsewhere: Remote bomb
Gives you detonator when planted
###			C	Set Time Bomb
Based on and consumes Time Bombs in inventory. NPC starts with one.
###			C	Tamper
- Interface works but reticle is green for non-tamperable items.
  - Log message "Not implemented yet", fair enough
###			√	Muscle
Complete
###			√	Intruder
Complete
###			√	Decoy
Complete
###			√	Cyber-Intruder
Complete
##		C	Interaction
###			C	Buy Slave
Pending actual assignment of owned slaves 
###			C	Cybernetic Surgery
Curated Trait-seller
###			C	Heal All
Like Doctor heal, but all in party with calculated price
###			C	Heal Other
Like Doctor heal, but activates reticle so you can select a party member or other.
###			C	Heal Partial
Like Doctor heal, but at a Blood Bag level.
####			C	Mouseover Price
Show price to heal targeted agent before selection
###			C	Quest Giver
New
###			C	Refill Guns
New
###			C	Repair Armor
New
###			C	Repair Weapons
New
###			C	Pay respects to Faction
Costs $1,000 to bump reputation up one level (Hostile → Annoyed etc)
"Improve Faction Relations"
Should only allow for 1 of these to simplify algorithm
But this means you'll need further faction traits
###			C	Sell Intel to Faction 
Reverse of buying into faction. Just a way to get cash in exchange for slightly reducing your relation. Friendly or better.
###			C	Start Election
New
###			C	Summon Professional
New
Pay a fee for him to teleport a Hacker, Thief, Doctor or Soldier to you. You still have to pay them to hire them.
###			C	This One's On Me
Buy a round for a patron and anyone with the same owner ID in the chunk.
No drink for that guy in the corner. Fuck that guy.
###			C	Train Attributes (Split to each)
New
###			C	Defense
New
Sell traits for double their Upgrade Machine cost
###			C	Guns
New
###			C	Melee
New
###			C	Movement
New
###			C	Social
New
###			C	Stealth
New
###			C	Trade
New
###			C	Visitor's Badge
Set Bribe options on separate traits
###			√	Administer Blood Bag
Complete
###			√	Borrow Money
Complete
###			√	Borrow Money (Moocher)
Complete
###			√	Bribe Cops
Complete
###			√	Bribe For Entry (Alcohol)
Complete
###			√	Buy Round
Complete
###			√	Give Blood
Complete
###			√	Heal (Player)
Complete
###			√	Identify
Complete
###			√	Influence Election
Complete
###			√	Leave Weapons Behind
Complete
###			√	Manage Chunk
####			√	Arena
Complete
####			√	Deportation Center
Complete
####			√	Hotel
Complete
###			√	Offer Motivation
Complete
###			√	Pay Debt
Complete
###			√	Pay Entry Fee
Complete
###			√	Play Bad Music
Complete
###			√	Use Blood Bag
Complete
##		C	Interaction Gate
###			C	Insular
Requires Faction Friendly
###			C	Insularer
Requires Faction Loyal
###			C	Insularest
Requires Faction Aligned
###			√	Untrusting
Complete
###			√	Untrustinger
Complete
###			√	Untrustingest
Complete
##		C	Loadout
###			C	CC Items Inclusion
Any items added to the character in the CC will be included when spawned in a chunk. In vanilla, they are overridden by the chunk-defined inventory.
###			C	Guns_Common
New
###			C	Deep Storage
Doesn't drop items on death (They're in their butt)
###			C	Discreet
Automatically applies Silencer to all held weapons on load
###			C	Infinite Ammo
New
###			C	Infinite Consumables
New
###			C	Infinite Durability
New
###			C	Item Groups
uwumacaronitime's idea: Item groups similar to NPC groups

I can see this going two ways: 
- As a trait for NPCs to generate with
- As a designated item in the chunk creator for use in NPC & Object inventories. 

I am leaning towards implementing both of these. But whichever is chosen, make it very clear to avoid confusion.

Vanilla list:
- Defense
- Drugs
- Food
- Guns
- GunAccessory
- Melee
- Movement
- NonViolent
- NonUsableTool
- NonStandardWeapons
- NonStandardWeapons2
- NotRealWeapons
- Passive
- Social
- Stealth
- Supplies
- Technology
- Trade
- Usable
- Weapons
- Weird
###			√	Manager Key
Complete
###			√	Manager Mayor Badge
Complete
###			√	Manager Safe Combo
Complete
##		C	Merchant Buyer Modifiers
###				C	Buyer Cap
Total amount of money the trader will dispense when buying player goods
Vanilla For Sellomatic: 250-300 per level
####				C	$50
####				C	$150
####				C	$500
####				C	$1000
####				C	Unlimited
###				C	Buyer Type
A shorter list of broader categories than Merchant Type. Depends on how it feels while you write it.
###				C	Buyer Type - Match Merchant Types
Mirror shop inventory
##		C	Merchant Type
###			C	00 Refactor
New
###			C	00 Custom quantity in refactor
Allows to sell shitty items in junk dealer, for instance
###			C	NPC Loadout
Since Character Creator inventory isn't by default carried to spawn, use it as a shop inventory.
###			C	Player Loadout 
As in, the inventory you'd see in a Loadout-o-matic as a shop inventory
##		C	Relationships - Faction
###			C	00 Refactor
Put custom methods in faction traits.
E.g. Crushable.IsAlignedTo(Agent agent)
agent == vagent.crepe || agent.hastrait<crushable>
This logic can get very ugly so it'd be nice to pack it away elsewhere and just iterate through all applicable traits.
###			C	Faction Firefighter
###			C	Faction Cannibal
###			C	Faction Military
###			C	General concept
Friendly to faction doesn't align you. You do not inherit the faction's relationships.
Loyal causes you to inherit its relationships, but negative ones are moderated:
	Hostile → Annoyed
	Annoyed → Neutral
		The net effect of this one is that Loyalists are less likely to initate conflict on behalf of their faction.
Aligned means you fully inherit any faction-mandated relationships.
###			C	Config Files for unique player-defined factions
Generate traits based on these names
Allow multiple faction list files in a folder, to increase ease of compatibility.
###			C	00 Refactor
These should inherit from a shared class
public override char Faction => '1', etc.
public override string Relationship => VRelationship.Aligned, etc.
###			C	Faction 1 Annoyed
###			C	Faction 1 Friendly
###			C	Faction 1 Loyal
###			C	Faction 2 Annoyed
###			C	Faction 2 Friendly
###			C	Faction 2 Loyal
###			C	Faction 3 Annoyed
###			C	Faction 3 Friendly
###			C	Faction 3 Loyal
###			C	Faction 4 Annoyed
###			C	Faction 4 Friendly
###			C	Faction 4 Loyal
###			√	Faction 1 Aligned
###			√	Faction 1 Hostile
###			√	Faction 2 Aligned
###			√	Faction 2 Hostile
###			√	Faction 3 Aligned
###			√	Faction 3 Hostile
###			√	Faction 4 Aligned
###			√	Faction 4 Hostile
##		C	Relationships - General
###			C	All-Annoyed
New
###			C	All-Friendly
New
###			C	All-Hostile
New
###			C	Never Aligned
For all Never traits, 
###			C	Never Annoyed
###			C	Never Friendly
###			C	Never Hostile
###			C	Never Loyal
###			C	Never Aligned
###			C	Never Submissive
###			C	Forgetful
Returns to neutral after a timer
###			C	Imposterous
After a long delay, goes hostile to any they're aligned to
###			C	Long Fuse
Hostile at 12 strikes, Annoyed at 6
###			C	Longer Fuse
Hostile at 8 strikes, Annoyed at 4
###			C	Longest Fuse
Goes annoyed only when damaged. Goes hostile only when annoyed and damaged.
###			C	Protect at Aligned
Inner circle only
###			C	Protect at Friendly
Indeed
###			C	Protect at Neutral
Cosmopolitan
###			C	Protect at Annoyed
What a good guy
###			C	Protect never
Well okay damn dude
###			C	Short Fuse
Hostile at 4 strikes, Annoyed at 1
###			C	Shorter Fuse
Hostile at 2 strikes, Annoyed at 1
###			C	Shortest Fuse
So annoyed they go straight to hostile at 1 strike
###			√	Hostile to Cannibal
Complete
###			√	Hostile to Soldier
Complete
###			√	Hostile to Vampire
Complete
###			√	Hostile to Werewolf
Complete
###			√	Relationless
Complete
##		C	Senses - Hearing
###			C	Deaf
Already a status effect, you might even overlap the trait name as a cool trick
##		C	Senses - Vision
###			C	Scanline
Show NPC vision cone like Cop bot, minus the audio (that can be modularized elsewhere)
###			C	Short-sighted
Short vision
###			C	Far-sighted
Camera-style blindspot
###			C	Eye of the Hawk
Long vision
###			C	Eye of the Duck
Wide peripheral vision
###			C	Eye of the Worm
Narrow peripheral vision
##		C	Spawn
###			C	Enslaved
New
###			C	Hide In Object
Detect Bush/Manhole on same tile
###			C	Respawn
Never-ending waves of enemies
####			C	Respawn Delay
0, 3, 15, 30, 60, 120, 180 seconds
####			C	Respawn Quantity
1, 3, 10, Infinite
###			C	Roaming Gang
New
###			C	Slave Owner
NEw			
###			C	Spawn on Object Destruction
Set matching Owner ID to spawn like Ghost from Gravestone
##		C	Spawn - Bodyguarded
New
- There are a few other hits that came up in a string search (possibly "Musician"):
  - LoadLevel.SpawnStartingFollowers
  - ObjectMult.StartWithFollowersBodyguardA
    - Ignore this one, it's for the Player Bodyguard trait
- Check out ObjectMult.StartWithFollowers, there are something like 4 similarly named methods in there
###			C	Pilot Trait
No errors, but no effect.
###			C	Bodyguard Quantity Traits?
One / few / many, that's it
###			C	Bodyguarded - Cop
New
###			C	Bodyguarded - Blahd
New
###			C	Bodyguarded - Crepe
New
###			C	Bodyguarded - Goon
New
###			C	Bodyguarded - Gorilla
New
###			C	Bodyguarded - Mafia
New
###			C	Bodyguarded - Soldier
New
###			C	Bodyguarded - Supercop
New
###			C	Bodyguarded - Supergoon
New
##		C	Tethers
###			C	Types depend on vanilla variable
##		C	Trait Gates
###			C	Crust Enjoyer
If you have Upper Crusty, this character is Loyal
I think this is actually automatic with Enforcer
###			C	Gate Vendor
Won't sell unless you have appropriate trait
###			C	Gate Hire
Won't hire unless you have appropriate trait
###			√	Cool Cannibal
Complete
###			√	Bashable
Complete
###			√	Common Folk
Complete
###			√	Crushable
Complete
###			√	Cop	Access
Complete
###			√	Family Friend
Complete
###			√	Honorable Thief
Complete
###			√	Scumbag
Complete
###			√	Slayable
Complete
###			√	Specific Species
Complete
###			√	Suspicious Suspecter
Complete
#		C	Mutators

Setting: Force Big Quest completion - If set to yes, you will need to complete your big quest before leaving the floor, if there is one. You will not be able to exit the floor until the BQ is done, even after completing all the missions on the floor. If the quest is failed (floor only fails dont count) you will spontaneously combust and die.

Setting: Exit on Death - If set to yes, you will be forced to exit the floor when you die. On the next floor, you will be revived and you will be healed to maximum HP.

Mutator: Exit Timer - Similar to Time Limit, except that the elevator is locked until the timer reaches zero. Missions will not open the elevator.

Mutator: Exit Timer EXTREME - Similar to Time Limit EXTREME, except that the timer is LONGER, and the elevator is locked until the timer reaches zero. Missions will not open the elevator.

Mutator: Exit Timer LITE - Similar to Time Limit, except that the timer is SHORTER, and the elevator is locked until the timer reaches zero. Missions will not open the elevator. 

##		C	00 Mutator List Population
###			C	00 Hide from Non-Editor access
- CreateMutatorListCampaign
###			√	General Mutator List
- Show up in LevelEditor UI, may be a manually constructed list
- LoadLevel.loadStuff2 @ 171
##		C	Branching
Basically allows options at Exit Elevator to choose the next level, and/or skipping levels on the level list

CampaignData
	levelNameList		List of strings
	levelList			List of LevelData
	mutatorList			List of strings
GC.sessionDataBig.curLevelEndless - 1
	Seems to be the main int counter of what level you're on

- LoadLevel
  - loadStuff2 @ 199
    - this.customLevel = this.customCampaign.levelList[n];

663:
	if (this.customCampaign.levelList[n].levelName == this.customCampaign.levelNameList[this.gc.sessionDataBig.curLevelEndless - 1])
		Transpile-replace the right side of this assignment to a custom method call that determines the new target level's int ID in the campaign level list

###			C	00 Usage Guide
This one will be complicated to explain, so it's best to go overboard on documentation and provide examples.
###			C	Label Alpha/Beta/Gamma/Delta
Labels a level
Mutually exclusive
###			C	Exit Alpha/Beta/Gamma/Delta per Flag A/B/C/D
If target boolean is true, exit to target level
###			C	Exit Alpha/Beta/Gamma/Delta
Destination at Elevator
###			C	Exit +1/2/3/4
Can have multiple, to allow Branching
Adds options at Elevator
###			C	Flag A/B/C/D false
For level access
###			C	Flag A/B/C/D true
For level access
###			C	Level Entry Requires A/B/C/D false
Gate level access
###			C	Level Entry Requires A/B/C/D true
Gate level access
###			C	Traits for Level Branching
##		C	Disasters
###			C	Random Disaster
Random Disasters and/or Disasters Every Level aren't offered in the editor
##		T	Followers
###			C	Homesickness Disabled
####			C	Set to Aligned
###			T	Homesickness Mandatory
Test
##		C	Gameplay
###			C	No Funny Business
For town levels. Ensures no one will be killed.
You will need to eliminate spontaneous hostiles for this to work, though.
##		C	Interface
In PlayerControl.Update there's a hidden keystroke for pressedInterfaceOff
###			C	No Minimap
##		C	Presets
Curated configurations of challenges that can serve as a shortcut.
E.g., "Interlude:" No funny business, pause big quests, etc.
##		C	Progression
###			C	Exit Timer
For Survival Levels, elevator is locked until timer elapses.
###			C	Delay Trait Gain
Count and put off trait choices until this challenge isn't present
###			C	Reset Player Character
###			C	Empty Inventory on Exit
###			C	Wipe Statistics on Exit
##		C	Quests
###			C	Big Quest Exempt
Deactivate Big Quest for level, freeze mark counts
###			C	Big Quest Mandatory
Complete quest to exit level
Die if you fail quest
Possibly modified by Mark-based
###			C	Big Quest Stopping Point
Equivalent to Mayor's Village, where super special abilities activate if you completed the Big Quest
###			C	Major Contract
Main quest rewards are multiplied by 10
##		C	Utility
###			C	Sort active Mutators by Name
- ScrollingMenu.PushedButton @ 0006
  - Pretty much has exactly what you need.
#		C	Item Groups
wut
#		CT	Object Additions
##		CT	Custom Object variables
###			C	Containers
####			C	Lockable
Only Desk seems eligible, but that's enough to go for it. Use stringvar1 or something as a Locked variable
Maybe generate a Desk Key item, specific to the Desk
####			C	Note loads in ShowChest interface
WorldSpaceGIU.ShowChest
		interactingAgent.mainGUI.invInterface.chestDatabase = invDatabase;
We should be able to filter that somehow

Get some info on that watergun item, make it into a custom Clue item, see how we can identify it to filter it out.
#####				C	InvDatabase.IsEmpty
202207241004 - Filter IsEmpty to return true when the only item is a Clue
#####				C	InvDatabase.TakeAll 
Exclude Clue or whatever
#####				C	InvInterface.UpdateInvInterface
			using (List<InvItem>.Enumerator enumerator = this.chestDatabase.InvItemList.GetEnumerator())
Filter InvItemList here. Inject a call to a custom method.
#####				C	InvInterface.Slots
This is a List<InvItem> and I think what we need to filter
####			C	Note drops as Water Gun when object destroyed
####			√	Open Container
####			√	Drop Contents when destroyed
####			√	Show Editor Controls
####			√	Load/Save Editor Input
###			C	Readables
####			C	Movie Screen
Didn't work yet: https://discord.com/channels/187414758536773632/433748059172896769/1000014921305706567
Should be ready with next RL release
####			C	Custom Sprites when readable text present
Will need a visual indicator to the player. This is an extra, if RL correctly toggles interactability conditional on valid interactions.
#####				C	Computer
"Unread Mail" icon on screen
####			√	Input field Display
P_LevelEditor.UpdateInterface
####			√	Input field Edit
P_LevelEditor.PressedLoadExtraVarStringList
####			√	Setup object
P_BasicObject.Spawn
####			√	Interaction
Readables.Setup
##			C	Air Conditioner
Enable "Fumigating" w/ staff in gas masks as option
GasVent.fumigationSelected
##			C	Fire Hydrant
Ability to be already shooting water according to direction
##			C	Explosive Barrel
###				C	Sprite Warning Label
Specific to Status Effect
If label too small, use barrel color and pattern
###				C	Explosion Type (ExtraVarString)
This will be important since this list will be used for several objects.
###				C	Durability (ExtraVarString2)
This can also be reused, e.g. for security cam
####				C	Bombproof
Not useful for barrels, but since this list will be reused
####				C	Reinforced
Requires 2-3 hits to explode rather than 1
(Normal for most objects, generally only useful for barrels)
####				C	Steel-Cased
Requires an explosion to destroy
####				C	Volatile
Destroyed if you bump into it
##			C	Laser Emitter
###				C	Mode: Metal Detector
This would really only make sense with a Stop & Frisk mod.
##			C	Slime Barrel
###				C	Sprite Warning Label
Specific to Status Effect
If label too small, use barrel color and pattern
###				C	Status Effect (ExtraVarString)
This will be important since this list will be used for several objects.
###				C	Durability (ExtraVarString2)
This can also be reused, e.g. for security cam
####				C	Bombproof
Not useful for barrels, but since this list will be reused
####				C	Reinforced
Requires 2-3 hits to explode rather than 1
(Normal for most objects, generally only useful for barrels)
####				C	Steel-Cased
Requires an explosion to destroy
####				C	Volatile
Destroyed if you bump into it
##			C	Turret
###				C	Gun Type (ExtraVarString)
####				C	Flamethrower
####				C	Freeze Ray
####				C	Ghost Gibber
####				C	Grenade Launcher
####				C	Machine Gun
####				C	Minigun
####				C	Rail Gun
####				C	Rocket
####				C	Shrink Ray
####				C	Sniper
Extends vision range
####				C	Tranquilizer
####				H	Request - Custom Dart
Streets of Cheese suggested darts with choosable status effect
Doing entries for each type would be prohibitive, but if one ExtraVarString looks sparse you could use that for the status effect. And that'd likely be reusable code since other objects might use statuses.
###				C	Sensors (ExtraVarString2)
####				C	Built-In Camera (Rotating)
####				C	Built-In Camera (Stationary)
###				C	Durability (ExtraVarString3)
####				C	Armored
####				C	Fragile
####				C	Indestructible
###				C	Other (ExtraVarString4)
####				C	Explode on Death
#		√	Bug Archive
##			√	Fast or Rollerskates won't fall in Hole
###				√	Fix
Vanilla bug
##			H	Relationships not Loaded
###				H	Fix
Everything is running, but the Agent Hooks don't exist when running from a Save. Abbysssal said this is out of scope, so this bug is probably here to stay for the time being.
#		√	Trait Archive
##			√	Cost Scale
###				√	Less
Complete
###				√	More
Complete
###				√	Much More
Complete
###				√	Zero
Complete
##			√	Relationships - Player
###				√	Player Aligned
Complete
###				√	Player Annoyed
Complete
###				√	Player Friendly
Complete
###				√	Player Hostile
Complete
###				√	Player Loyal
Complete
###				√	Player Secret Hate
Moved to Behavior - Ambush (more transparent to user)
###				√	Player Submissive 
Complete