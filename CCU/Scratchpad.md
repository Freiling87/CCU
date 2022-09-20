#			How to read this
If you wandered in here out of curiosity, this is my working notes file, and completed/planned feature list. It's a markdown file, but best viewed in raw form since a lot of its organization has to do with text characters' alignment.

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features

HEY BRO ALT+UP TO JUMP TO METHOD SIGNATURE
#		Scope
##		C	Permanent Status Effects
###			C	Giant
###			C	Enraged
##		C	Restrict Trait Count 
to legalize characters
##			T	Custom Decal Pilot
I put a Custom Floor Decal item in the Editor Object List. Nothing else. See what kind of errors pop up to determine what to patch.
##			P	v. 1.0.1 Patch Notes
- Bugfixes
  - Default Goals
	- Scene Setters are no longer interrupted by "Flee" behaviors.
  - Objects
	- Containers
	  - Can no longer loot via hack (Lame!)
	  - Disabled redundant Open/Search buttons.
	- Investigateables
	  - Computer no longer interrupts hacking interaction when Investigation text is set.
	  - Investigate button no longer appears with blank text.
  - Traits
	- Grab Everything: No longer grabs active traps, unless agent has Accident-Prone
	- Extortable: Multiple Extortables with Matching Owner ID in same chunk are now handled as a group rather than separately. Duplicate Extortion buttons eliminated.
	- Heal: No longer says broken dialogue when rejecting interaction
	- Language Speaker group: Removed from upgrade menu in Augmentation Booth. This is a planned feature that was meant to be postponed.
	- Rel Faction: Sharing faction animosity can no longer improve your relations with others. 
	- Pay Cop Debt: Button text fixed.
	- Cost Scale: Now excludes Bribe for Quest Item.
	- Relationship: No longer affect Home Base
- Feature Additions
  - Default Goals
	- Random Teleport 
  - Objects
	- The Vendor Cart is no longer accessible as a normal container. It requires an operating bar to access, and will make a small noise as you ransack it. Sneaky/Silent Fingers both apply.
  - Traits
	- Appearance
	  - You can now randomize the appearance of NPCs. See the Traits page for details, as there are too many traits to list here.
	  - Feature limitation: Eye type may change for Sitting/Dancing NPCs. This is a bug (IMO) in the original game code. Specifically: Narrow, Wide, Dead eyes are affected; Zombie & Cannibal eyes are not.
	- Hire Duration
	  - Permanent Hire
	  - Permanent Hire Only
	- Merchant Stock: Various modifiers for the quantity/durability of sold items.
	  - Clearancer
	  - Masterworker
	  - Masterworkerer
	  - Masterworkerest
	  - Shiddy Goods
	  - Shoddy Goods
	  - Wholesaler
	  - Wholesalerer
	  - Wholesalerest
	- Merchant Type: Selecting multiple types will now combine inventory random-choice pools rather than just selecting one of them.
	  - Burger Joint
	  - Sugar Shack
##			P	v. 1.0.1 Documentation Additions
Note that the Header tiers are extended by one in Scratchpad, so you'll need to adjust those.
###				Object Variable List
Vendor Cart mechanics note
###				Appearance
There are a *lot* of traits in this category, grouped into the following. They have abbreviated prefixes, in order to keep sorting together but save some space. I'm not going to list every single trait in here, just the unusual ones.

- App AC1: Accessory
- App BC1: Body Color
- App BC3: Body Color Special
- App BT1: Body Type
- App BT2: Body Type Greyscale
- App EC1: Eye Color
- App ET1: Eye Type
- App FH1: Facial Hair
- App HC1: Hair Color
- App HC2: Hair Color Grouped
- App HS1: Hairstyle
- App HS2: Hairstyle Grouped
- App LC1: Legs Color
- App LC3: Legs Color Special
- App SC1: Skin Color
- App SC2: Skin Color Group

The "Group," uh... groups... have multiple items. "Long hairstyles," "Punk hairstyles," you get it, you're smart. These are just offered as a quicker way to customize appearance without having to do it granularly. Their contents are listed in sections below.

All selections are thrown into a random pool. When the NPC is generated, it pulls a random selection from this pool for its appearance. They are compatible, so you could add Mohawk as well as Punk Styles if you want to slightly weight the pool, but you won't have a ton of control over that. 

####				How it works
By adding an appearance trait, you add an item to a random selection pool. When the agent is spawned as an NPC, its appearance is randomly chosen from all pools with items in them. Empty pools are ignored, and appearance copies the visuals in the character editor.

####				Best Practices
#####					Test Sheet
With so many random variables, it's hard to tell whether the look is perfect unless you see a ton of examples. Use a chunk with a ton of copies of the same NPC to identify weird edge cases and gradually shape a population aesthetic. The bonus is that if you decide to share this character on the workshop, you can use that picture to give a more accurate picture of what the character will look like.

####				BC3: Body Color Special

|Trait								|Effects								|
|:----------------------------------|:--------------------------------------|
|Shirtless							|Body color matches skin color
|Shirtsome							|Body color is rerolled if it matches skin color

####				HC2: Hair Color Groups

|Hair Color			|Normal	|Normal (No Grey)	|Wild	|
|:------------------|:-----:|:-----------------:|:-----:|
|Black				|✓		|✓					|		|
|Blonde				|✓		|✓					|		|
|Blue				|		|					|✓		|
|Brown				|✓		|✓					|		|
|Green				|		|					|✓		|
|Grey				|✓		|					|		|
|Orange				|✓		|✓					|		|
|Pink				|		|					|✓		|
|Purple				|		|					|✓		|
|Red				|		|					|✓		|
|White				|		|					|		|

####				HC3: Hair Color Special

|Trait								|Effects								|
|:----------------------------------|:--------------------------------------|
|Matched Mask						|If a Hoodie is rolled for Hairstyle, matches its color to Body Color
|Uncolored Mask						|If a non-hairstyle is rolled (Hoodie, Gorilla head, etc.), it won't be colored

####				HS2: Hairstyle Groups

|Hairstyle			|Balding|Bangs	|Female	|Long	|Male	|Not Hairstyles	|Punk	|Short Female	|Short	|Stylish|
|:------------------|:-----:|:-----:|:-----:|:-----:|:-----:|:-------------:|:-----:|:-------------:|:-----:|:-----:|
|Afro				|		|		|		|		|✓		|				|		|				|		|		|
|Alien Head			|		|		|		|		|		|✓				|		|				|		|		|
|Assassin Mask		|		|		|		|		|		|✓				|		|				|		|		|
|Bald				|✓		|		|		|		|✓		|				|✓		|				|		|		|
|Balding			|✓		|		|		|		|✓		|				|		|				|		|		|
|Bangs Long			|		|✓		|✓		|✓		|		|				|✓		|				|		|		|
|Bangs Medium		|		|✓		|✓		|✓		|		|				|✓		|				|		|		|
|Butler Bot Head	|		|		|		|		|		|✓				|		|				|		|		|
|Cop Bot Head		|		|		|		|		|		|✓				|		|				|		|		|
|Curtains			|		|		|		|		|✓		|				|		|				|		|		|
|Cutoff				|		|		|✓		|		|		|				|		|				|		|		|
|Flat Long			|		|		|✓		|✓		|		|				|		|				|		|		|
|Gorilla Head		|		|		|		|		|		|✓				|		|				|		|		|
|Hobo Beard			|		|		|		|		|		|				|		|				|		|		|
|Hoodie				|		|		|		|		|		|✓				|		|				|		|		|
|Killer Robot Head	|		|		|		|		|		|✓				|		|				|		|		|
|Leia				|		|		|✓		|		|		|				|✓		|✓				|		|		|
|Messy Long			|		|		|✓		|✓		|		|				|		|				|		|		|
|Military			|		|		|		|		|✓		|				|		|				|✓		|		|
|Mohawk				|		|		|		|		|✓		|				|✓		|				|		|		|
|Normal Hair		|		|		|		|		|✓		|				|		|				|✓		|		|
|Normal High		|		|		|		|		|✓		|				|		|				|✓		|		|
|Pompadour			|		|		|		|		|✓		|				|		|				|		|✓		|
|Ponytail			|		|		|✓		|		|		|				|		|✓				|		|		|
|Puffy Long			|		|		|✓		|✓		|		|				|		|				|		|		|
|Puffy Short		|		|		|		|		|✓		|				|		|				|		|		|
|Robot Head			|		|		|		|		|		|✓				|		|				|		|		|
|Sidewinder			|✓		|✓		|		|		|✓		|				|		|				|		|		|
|Slavemaster Mask	|		|		|		|		|		|✓				|		|				|		|		|
|Spiky				|		|		|		|		|✓		|				|✓		|				|✓		|✓		|
|Spiky Short		|		|		|		|		|✓		|				|✓		|				|✓		|✓		|
|Suave				|		|		|		|		|✓		|				|		|				|		|✓		|
|Wave				|		|		|✓		|✓		|		|				|		|				|		|		|
|Werewolf Head		|		|		|		|		|		|✓				|		|				|		|		|

####				HS3: Hairstyle Special

|Trait								|Effects								|
|:----------------------------------|:--------------------------------------|
|Masks 50%							|If the hairstyle pool contains any Masks, raises their likelihood of being drawn to 50%.

####				LC3: Legs Color Special

|Trait								|Effects								|
|:----------------------------------|:--------------------------------------|
|Pantiful							|Legs color is rerolled if it matches skin color
|Pantsless							|Legs color matches skin color

####				SC2: Skin Color Groups

|Skin Color			|Human	|Shapeshifter	|Vampire|Zombie	|
|:------------------|:-----:|:-------------:|:-----:|:-----:|
|Black				|		|				|		|		|
|Blue				|		|				|		|		|
|Brown				|		|				|		|		|
|Gorilla			|		|				|		|		|
|Green				|		|				|		|		|
|Grey				|		|				|		|		|
|Human Black		|✓		|				|✓		|		|
|Human Gold			|✓		|				|		|		|
|Human Light Black	|✓		|				|		|		|
|Human Mixed		|✓		|✓				|		|		|
|Human Pale			|✓		|✓				|		|		|
|Human Pink			|✓		|✓				|		|		|
|Human Super Pale	| 		|				|✓		|		|
|Human White		|✓[^1]	|✓[^1]			|		|		|
|Mech				|		|				|		|		|
|Orange				|		|				|		|		|
|Pink				|		|				|		|		|
|Purple				|		|				|		|		|
|Red				|		|				|		|		|
|Werewolf			|		|				|		|		|
|White				|		|				|		|		|
|Yellow				|		|				|		|		|
|Zombie 1			|		|				|		|✓		|
|Zombie 2			|		|				|		|✓		|

[^1]: Doubled in this group. Just copying vanilla.

###				Hire Duration

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Permanent Hire										|- Offers to hire at 8x the normal cost, with infinite uses of Expert ability.
|Permanent Hire Only								|- As Permanent Hire, but removes original one-time hire option.

###				Merchant Stock
Traits in this category are multiplicative.

***Durable Wares:** Melee & Ranged weapons, Wearables*

***Stackable Wares:** Consumables, Food, Tools, Throwing Weapons*

|Trait												|Effect													|
|:--------------------------------------------------|:------------------------------------------------------|
|Clearancer											|- Allows the same item to be sold multiple times, like Bartender. Better for smaller inventories.
|Masterworker										|- Durable wares have 2x Durability
|Masterworkerer										|- Durable wares have 3x Durability
|Masterworkerest									|- Durable wares have 4x Durability
|Shiddy Goods										|- Durable wares have 1/3x Durability
|Shoddy Goods										|- Durable wares have 2/3x Durability
|Wholesaler											|- Stackable wares have 2x Quantity
|Wholesalerer										|- Stackable wares have 3x Quantity
|Wholesalerest										|- Stackable wares have 4x Quantity
##			P	Bugs
Except crickets, crickets are fine.
###				C	Random Teleport didn't work
	[Info   : Unity Log] SETUPMORE4_13 False
	[Info   : Unity Log] SETUPMORE5
	[Info   : Unity Log] FADE
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	CCU.Systems.CustomGoals.CustomGoals.RunSceneSetters () (at <aae0405f671f4f6bbf10453a674b1f29>:0)
	CCU.Patches.Level.P_LoadLevel.SetupMore5_Postfix (LoadLevel __instance) (at <aae0405f671f4f6bbf10453a674b1f29>:0)
	LoadLevel.SetupMore5 () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	LoadLevel+<SetupMore4_2>d__150.MoveNext () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
###				C	Can open empty containers
Shows empty list
###				C	Big Bomb Blacement Bug
Disaster Big Bomb placed a bomb in Vent
	Too easily accessible
	Allowed player to keep it
###				C	Fac Rel Refactor
This might be different from the original enemy of my enemy bug. Test before clearing.
Agent 1 - Wanted / Hostile Fac1
Agent 2 - Law / Hostile Fac2 
Should be hostile, but Rel Fac set to neutral.
###				C	SORCE Mutators not available in Editor levels 
I remember slating this to be done for CCU but never had to. There might be a shortcut to automate this for SORCE if you copy the formatting from CCU.
###				C	Container button text
On loading a chunk, they don't pull and display in the button. They are still in the item, and drop correctly.
P_LevelEditor.SetNameText_Prefix: tileNameText is blank unless defined this session. I.e., item name is not loading here.
P_LevelEditor.ShowCustomInterface: itemName is always blank, which I think means that:
	UpdateInterface_OnSelect_ShowCustomInterface: Loc_43 is not loading what you think it is. tileNameText2 is apparently always blank, but you might be using the default arg wrong for a harmony patch.
###				C	Clone Boys Dealer Shop Empty
I think rLists are being blocked

	[Debug  :CCU_TraitManager] TRAIT LIST: Clone Boys Dealer
	[Debug  :CCU_TraitManager]      Scumbag 
	[Debug  :CCU_TraitManager]      Borrow_Money_Moocher
	[Debug  :CCU_TraitManager]      Wildcard
	[Debug  :CCU_TraitManager]      Drug_Dealer
	[Debug  :CCU_TraitManager]      Guilty
	[Debug  :CCU_TraitManager]      IdentifyScience
	[Debug  :CCU_TraitManager]      HatesBlahds
	[Debug  :CCU_TraitManager]      HatesCrepes
	[Debug  :CCU_TraitManager]      Faction_3_Aligned
	[Debug  :CCU_TraitManager]      Meat_Chunks
	[Debug  :CCU_P_Agent] Inventory:
	[Debug  :CCU_P_Agent] Money
	[Debug  :CCU_P_Agent]
	[Debug  :CCU_P_Agent]
	[Debug  :CCU_P_Agent]
	[Debug  :CCU_P_Agent] 
###				C	Player Character speaks all
Tried something
###				C	False "Inventory Full" message
Trying to pick up Whiskey, Replicant Rebel character
Able to replicate?
###				H	Containers Show "(Empty)"
Start_SetupInvDatabasesForContainers sets chestReal to true. This makes it so that (Empty) does display the first time the chunk is run, but not on re-runs.
	This bool is allowing PlayfieldObject.MakeChestNonInteractable to be called, since it's already built to handle any object.
	Re-setting it to true in a PlayfieldObject.SetVars_Postfix didn't have any effect.
It might be the 0-Qty Money item throwing off InvDatabase.IsEmpty. That'd be the next thing to try.
###				H	Jukebox Hacks
Possibly RogueLibs, wait for confirmation.

Mambo:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables.PlayBadMusic () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	RogueLibsCore.VanillaInteractions+<>c.<Patch_Turntables>b__68_2 (RogueLibsCore.InteractionModel`1[T] m) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.SimpleInteraction`1[T].OnPressed () (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton2 (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.InteractionModel.OnPressedButton (System.String buttonName) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	RogueLibsCore.RogueLibsPlugin.PressedButtonHook (PlayfieldObject __instance, System.String buttonText) (at <ac95cf0d3a8543748b4a19536e8724e6>:0)
	Turntables.PressedButton (System.String buttonText, System.Int32 buttonPrice) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ObjectReal.PressedButton (System.String buttonText) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	WorldSpaceGUI.PressedButton (System.Int32 buttonNum) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.PushButton () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.DoUpdate (System.Boolean onlySetImages) (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	ButtonHelper.Update () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
Bladder: 
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	Turntables+<BadMusicPlayTime>d__31.MoveNext () (at <7fd7dd1709b64c98aabccc051a37ae28>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
###				H	Hiring Voucher not consumed
This is an RL bug, should be fixed ~08/29/2022.
###				H	$0 in container
https://discord.com/channels/187414758536773632/1003391847902740561/1007975536607383574
Maxior - Shelf w/ $0 as container, but not Trash Can
So far, unable to replicate
##		C	Appearance
###			C	Hair Color Special
####			C	Matched Masks
Color issue: Related to skin tone on body being copied to hair, which hair can't copy.
IS there a way to get those colors in there?

The issue is GetColorFromString. 
If you have a system that can go from string to color, you might greatly expand color choices here.
####			√	Uncolored Masks
Complete
###			C	Hairstyle Special
####			C	General Mask/Accessory compatibility
Might need to make a Dict<string, string[]> of these two sets, since compatibility seems to vary for each.
####			√	Mask Override (Rename)
Complete
####			√	Masks 50%
Complete
###			H	Randomization
Not possible yet - Hook regeneration.
You have access to CharaacterCreation.traitsChosen, but there is no complete character to pull actual traits from in this interface. So you're stuck with string names, and this feature isn't important enough to make a whole module for.
###			C	Footwear Color
agenthitbox.footwearcolor
agenthitbox.footwear1.gameobject.setactive()
###			√	General Traits
####			C	Static Preview
#####				C	Gym Dweller
Bpdy type is made static.
###			√	Accessory
####			√	No Accessory 50%
Complete
####			√	No Accessory 75%
Complete
###			√	Body Color
Complete
###			√	Body Color Special
####			√	Shirtless
Complete
####			√	Shirtsome
Complete
###			√	Body Type
Complete
###			√	Body Type Greyscale
Complete
###			√	Eye Color
Complete
###			√	Eye Type
####			√	Normal Eyes 50%
Complete
####			√	Normal Eyes 75%
Complete
###			√	Facial Hair
####			√	No Facial Hair 50%
Complete
####			√	No Facial Hair 75%
Complete
###			√	Hair Color
Complete
###			√	Hair Color Grouped
Complete
###			√	Hairstyle
Complete
###			√	Hairstyle Grouped
Complete
###			√	Legs Color
Complete
###			√	Legs Color Special
####			√	Pantiful
Complete
####			√	Pantsless
Complete
####			√	Pantsuit
Complete
###			√	Skin Color
Complete
###			√	Skin Color Grouped
Complete
#		CT	Projects
##			C	Enclave System
It's best to work on these piecemeal: mutators, traits, agents. Then combine them when the parts are complete.
###				C	Currency Changing
If you want different currencies, you'll need new ways for players to change it.
####				C	Trait: Behavior - Moneychanger
Cost Scaled
###				C	Enclave: Concrete Jungle
####				C	Agent: Bananamonger
Human vendor, speaks English and Goryllian, spawns with a Vendor cart. Sells bananas at a good price. Great place to change your currency.
####				C	Agent: Cheeky Monkey
Hides in the bushes, pickpockets. Like thieves but even more fucking annoying.
####				C	Currency: Banana
Yeah
####				C	Feature: Banana Vendor
One of the only vendors who takes cash. Sells at a good price. Great place to change money.
####				C	Floor Type: Grass
Yeah
####				C	Laws: Only Bitch Crimes are Illegal
Theft
Vandalism
etc.
Ritual Combat is respected as your right.
####				C	Wall Type: Concrete
Yeah
###				C	Enclave: District 69
####				C	Agent: Nordic
####				C	Agent: Reptilian
####				C	Agent: Small Grey
####				C	Agent: Glorbrax
Secretes alcohol in exchange for raw Moneys
####				C	Currency: Booze
####				C	Floor Type: Steel
####				C	Laws: 
Highly cosmopolitan, no tolerance of slavery...? 
Or maybe factionalized, with highly varying rules according to the chunk
Or maybe their laws are configured to treat you like a test subject.
####				C	Law Enforcers
Apprehension Ray is hard to hit, but teleports you immediately to jail
###				C	Enclave: Necropolis
####				C	Agent: Bloodslave
They don't even complain anymore
####				C	Agent: Necromancer
Expert hire, Can revive any dead agent for hire
####				C	Agent: Ghoul
Feral, eat corpses, hostile but cowardly
####				C	Agent: Sangrist
Currency Changer
####				C	Agent: Z-Rancher
Sells trained zombies, but that's way too OP
####				C	Currency: Blood
####				C	Feature: Canals
Catacomb vibe?
####				C	Floor Type: Dungeony stone
####				C	Wall Type: Brick
###				C	Enclave: Little Foreignia
The most normal of the Enclaves. Nearly identical to vanilla, but people speak Foreign.
###				C	Enclave: Werewales
Um
###				C	Enclave: Haywood & Mt. Blair 
This enclave's special challenge is that it's almost fully dominated by a single powerful faction, the Union.
The mob has a presence here, but the Union is generally not dirty. Generally.
Union refugees were the last group let inside the City before it completely sealed itself off from the outside world. They had missed the deadline, but brought so much important cargo that an exception was made. The shipping containers brought in in that refugee influx are still in use as small buildings. Their trucks ("Rigs") were broken down for parts, but the district retains the name.
####				C	Law: Seniority
Agents with the Old trait start out Friendly with the Law. They paid their dues.
####				C	Faction: Union
A French Vanilla faction, where members generally have Class Solidarity, No In-Fighting, and Confident in Crowds. 
They generally carry tools, power tools, and protective equipment. They don't carry the big guns, but make up for it in solidarity.
Outside Union Town, the Union is still pretty powerful. 
Cops and Firefighters are Union-Aligned, but not vice-versa.
####				C	Agent: Scab
New
####				C	Agent: Fink
Pinkertons
##			C	Language (Overhaul)
###			C	00 Mutator: Our Town
- NPCs have a chance to speak a foreign language
  - If a shopkeeper or bartender speaks a second language, generate a Neon Sign in front of their business in that language (we can dream, right?)
- NPCs have a chance to have Vocally Challenged
  - If they do, they always have at least one foreign language
- Some people become Friendly if you interact in a non-English language. Chance is higher if they don't speak English.
- Enables Polyglot trait choice
- Gives the Translator an actual reason to exist
- All hired agents can act as translators
- Every District has a set of Our Town mutators (below) that may trigger on level 2 of the district.
###			C	00 Our Town Non-Disasters
Gated behind Language System Mutator
Shifts the population so that about 66% are speakers, and one third - 1 half of those don't speak English.
Or you know what, make an overhaul mutator mapped to each class. That's what this is turning into.
- Concrete Jungle (Monkese)
- District 69 (ErSdtAdt)
- Little Faraway (Foreign)
- Meltingpot District (Even distribution of all language groups)
- Werewales (Werewelsh)
- Brainard (Lang Zonbi)
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
	Event Type
		Align
		Cry
		Die
		Drop Items
		Explosion (All existing types)
		Flee Level
		Hostile
		Path to Panic Room
		Random Teleport
		Reset Releationships
		Ruckus
	Event Delay
		Existing Explode on Death "Fuse" traits
		From Mac:
			[random] trigger only occurs 25% of the time. multiplicative with other [random]s
			[random] trigger only occurs 50% of the time. multiplicative with other [random]s
			[random] trigger only occurs 75% of the time. multiplicative with other [random]s
			[random] include duds for limits: failed random rolls count towards [limits]
			[random] only rolled once: the random roll is only cast once, at the start of the level, and the NPC will consistently always fail or succeed depending on that one roll.
			[limits] Only Once: Can only trigger once.
			[limits] Only Player: Only triggers if the triggerer was a player
			[limits] Only NPC: reversed Only Player
			[Special Interact Button] Only Aligned: Only available to aligned/hire-er agents.
			[Special Interact Button] Costs Money: The interaction costs money. All money- related traits apply.
			[Special Interact Button] Goto Agent/Object: Allows you to target an NPC or object. The Agent will then travel to there and trigger the event
			Apply to triggerer: apply all event effects to the triggerer instead of you
	Event Trigger
		Align
		Combat
		Committed Crime
		Damage Dealt
		Damage Taken
		Death
		Exit Level
		Interact
		Interact Option (named after Event Type)
		Join Party
		Out of Ammo
		Search
		Spawn
##			C	Dedicated section on Character Sheet
Should not be too hard, as the one method where it's filled out is pretty transparent
Just add a --- CCU TRAITS --- Divider or something
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
##			H	Stash System
On hold: Complete the Key/SafeCombo aspect of Containers first.
Locked + Keycoded really only apply to Desk. They might easily be left out of this system.
Variable field in editor, maybe with some basic setting variations (destructible, Investigateable, etc.)
Locks access to the object as a Chest unless the player holds the matching Stash Hint.
You don't know the object holds an item until you find the Stash Hint item somewhere. This could be in an Agent's inventory, or hidden elsewhere in the chunk. 
###				C	Item: Stash Hint
Generates on NPC inventory or in a container in their chunk
Acts like a key, but when activated the agent will read text out loud that's specific to a Container type
This will be EXTREMELY annoying with Gravestones, which is kinda funny
##			C	Sugar System
Merchant Type: Sugar Shack √
Passive: Keep Moving (Acts like Stinger, but doesn't betray you. Red herring)
Passive: Stinger (Calls cops and flees level if you sell or buy contraband)
And also the entire Drug Dealer mod series. 
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
#		C	Agent Goals
##			CT	Default Goals
###				C	Ambush
Hide behind object on same tile, if possible
###				C	Commit Arson
New
###				C	Frozen
###				C	Frozen (Permanent)
###				C	Robot Clean
DW
###				√	Arrested
Complete
###				√	Burned
Complete
###				√	Dead
Complete
###				√	Gibbed
Complete
###				√	Knocked Out
Complete
###				√	Random Teleport
Complete
#		C	Traits
##		C	Accent Color
Combine w/ Accent Effect traits
##		C	Accent Effect
Apply Accent Color trait to target effect
###			C	Agent Glow
Killer Robot has this
###			C	Monocolor Agent
E.g., if White is selected, they should look like a Greek Statue or whatever.
###			C	Nametag (Space/hover)
###			C	Vision Beam
New
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
##		C	Behavior
###			C	Consumer
Visits vending machines and merchants
###			C	Corpse Destroyer
Gibs enemies' corpses
###			C	Vanilla Panic Room behavior
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
###			C	Grab Weapons
New
###			C	Grab Contraband
New
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
###			C	Taunt
Cause all hostiles to target
###			C	Call
Crepe/Blahd call all nearby of same class
Do this, for same class/superclass, and for faction
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
##		C	Combat Patterns
###			C	Airstriker
Constant airstrike on enemy during combat
###			C	Chaaaaarge! 
Doable without special?
###			C	Fireworks Show
Random rockets, maybe sped up
###			C	Rocket Barrage
##		C	Combat Pattern Modifiers
For Boss-style multi-attacking NPCs
###			C	Choose Cycled Pattern
Repeats selected pattersn in a cirlce
###			C	Choose Random Pattern
Selects a random different pattern from all added
###			C	Pattern Interval 1
###			C	Pattern Interval 2
###			C	Pattern Interval 3
###			C	Pattern Interval 4
###			C	Pattern Trigger Allies Killed
###			C	Pattern Trigger Damage Taken
###			C	Pattern Trigger Health Threshold
##		C	Cost Currency
###			C	00 Button ExtraCost Display
Bananas & alcohol are hardcoded
To display them correctly, prefix WorldSpaceGUI.ShowObjectButtons (interprets magic numbers)
###			C	Banana
Test
###			C	Barter
Stretch goal, obv complicated
###			C	Blood
Blood Bags always an option
If Vampirism, allow drink
###			C	Booze
A la Bouncer
###			C	Flesh
Require Cannibalism? Maybe not
##		C	Drug Warrior
###			C	Feature: Werewolf Transformation compatibility
Not a bug, sorry
###			C	Feature: Apply multiple effects at once
This will be a little harder than expected. 
Try a prefix to GoalBattle.Process, but still use the vanilla flags to prevent redundancy.
###			C	Extendo-Wheels
Gain Roller Skates
###			C	Suicide Bomber
Initiate a 15s timer, then detonate a Huge explosion
Interface with Timer traits and Explosion traits to allow player to customize
###			C	Sweaty
Gain Wet, lmao
##		CT	Drug Warrior Modifiers
GoalBattle.Process is where the effect is applied.
###			√	Suppress Syringe AV
Complete
###			C	Extended Release
This might have differing effects on status effects. E.g., Fainting Goat Warrior might never trigger.
###			C	Eternal Release
New
###			C	One for the Road
Effect triggers when they would flee instead of at beginning of combat
####			C	Extended Release interaction
When paired with ER, the effect lasts until they would no longer be intimidated. 
###			C	Post Warrior
Effect triggers on end of threat (Regenerate, smoke, invisible)
###			C	High on Pain
Effect gains 1s of duration on taking damage
###			C	Take the Thrill Kill Pill
Effect gains 1s of duration on dealing damage, extra on kill
##		C	Explode On Death
System still works after minor refactor.
###			H	00 Doesn't trigger when exploded
Rocket launcher kill just gibbed them
This is vanilla behavior (See StatusEffects.ExplodeAfterDeathChecks). Only fix if there's an uproar.
###			H	Oil Spill
Shelved
Explosion.SetupExplosion ~373
DW:
	[Info   : Unity Log] ADDRELHATE: Custom (1134) (Agent) - Playerr (Agent)
	[Info   : Unity Log] SpawnExplosion -1
		No error message, but that's not a good thing
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
###			√	Cop Bot not Exploding
Complete
###			√	Certain explosion types don't delete body
P_StatusEffects_ExplodeBody.DisappearBody
###			√	Gib body
P_StatusEffects_ExplodeBody.GibItAShot
##		C	Explosion AV
###			C	Suppress Red Blink
No red blink before explosion
P_StatusEffects.ExplodeAfterDeathChecks transpiler for this.agent.objectSprite.FlashingRepeatedly
###			H	Cinematic Timer
Pending Ridiculous Explosion
##		C	Explosion Timer
Vanilla for EOD is 1.5 seconds
###			H	Cinematic Fuse
Pending creation of Ridiculous explosion.
###			√	Long Fuse
2x
###			√	Longer Fuse
3x
###			√	Longest Fuse
4x
###			√	Short Fuse
0.66x
###			√	Shorter Fuse
0.33x
###			√	Shortest Fuse
0.00x
##		C	Explosion Trigger
###			C	Combat Start
###			C	Death
###			C	Low Health
###			C	Spawn
This is more of a utility, to allow designers to explode or burn things at level start.
##		C	Gib Type
###			C	Robot Gibs
Need sprites :(
###			C	Wood
For use with Leaves, once they can combine into one
###			√	Ectoplasm
Complete
###			√	Gibless
Complete
###			√	Glass Shards
Complete
###			√	Golemite
Complete
###			√	Ice Shards
Complete
###			√	Leaves
Complete
###			√	Meat Chunks
Complete
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
###			C	Weather or Not
Use Mutiny timer for abandonment
###			C	Too Cool To Die
Agent can only be Knocked out, and can be revived
##		H	Hire Duration
A couple are technically Hire Triggers, so I broadened this cat name
###			C	Homesickness Immune
Automatic Homesickness Killer
###			C	Homesickness Enjoyer
Overrides Homesickness Killer
###			√	Permanent Hire
Complete
###			√	Permanent Hire Only
Complete
##		C	Hire Trigger
###			C	On Use Altar
"Activate Servants"
###			C	On Use Podium
"Who's With Me?!"
###			C	On Release
"Damn, I loved prison"
###			C	On Sight
"Another Survivor!"
###			C	On Start
"They told me to meet you at the station."
##		C	Hire Type
###			C	Interact
Any interaction option, target object.
Interaction choices are limited to theirs, not yours. This makes hired skills more useful (tech expert tampering, e.g.)
###			C	Chloroform
New
###			C	Cyber-Intruder (Up-Close)
No remote hack
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
###			C	Shitbird
Runs off with your money when you try to pay them for something, lol
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
###			C	Expansion request
Map Untrustings across Hire/Merchant/Service
There would only be nine, so this isn't TOO bad
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
##		C	Language
###				C	French Vanilla Gibberish
Make Goryllian say Ook Ook if you don't understand it 
Agent.SayDialogue:
	string text = this.gc.nameDB.GetName(this.agentName + "_NonEnglish", "Dialogue");

transpile agentName value to custom agentName method that returns appropriate gibberish.
	If agent has multiple, have them pick a random one to speak gibberish from.
###			H	Mute
No interactions under any circumstances, even with Translator
###			C	Polyglot
Gain 2 languages on trait gain, and an additional one every two levels.
If put on an NPC, those are randomly chosen.
####			C	Polyglot as Speaker +
Set as Upgrade for all Speaker traits
Also gives 2 languages on upgrade, not just 1. This is for consistency and so that it's actually worth upgrading.
####			C	Polyglot as Polyglot +
This is an interesting pattern that might be mirrored in other meta-traits. 
If they generally represent knowledge, then turn all knowledge skills into meta-traits. 
E.g. Medical Professional is split up into competencies with various healing items that have unique bonuses rather than just a flat HP gain.
####			C	Chance for Relationship Gain
Chance to make someone Friendly if they speak that language and not English
###			C	Telepathic (rename)
Can talk to all languages, even Mutes, unless they have some anti-telepathy trait or something
This could belong in a different category, since it has broad applications.
###			√	Speaks Chthonic
Complete
###			√	Speaks ErSdtAdt
Complete
###			√	Speaks Foreign
Complete
###			√	Speaks High Goryllian
Complete
###			√	Speaks Werewelsh
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
##		C	Merchant Stock
###			C	Replenisher
Replace stock with same item
###			C	Restocker
Replace stock with random
###			√	Clearancer
Allows repeats of items
###			√	Masterworker
2x Durability
###			√	Masterworkerer
3x Durability
###			√	Masterworkerest
4x Durability
###			√	Shiddy Goods
1/3 Durability
###			√	Shoddy Goods
2/3 Durability
###			√	Stocker
2x Qty
###			√	Stockerer
3x Qty
###			√	Stockerest
4x Qty
##		C	Merchant Type
###			C	00 Refactor
New
###			C	00 Custom quantity in refactor
Allows to sell shitty items in junk dealer, for instance
###			C	NPC Loadout
Since Character Creator inventory isn't by default carried to spawn, use it as a shop inventory.
###			C	Player Loadout 
As in, the inventory you'd see in a Loadout-o-matic as a shop inventory
##		CT	Passive
###			C	Elector
NPC has 2x effect on Electability
###			C	Unspecial
Disables:
	Possession
	Secret werewolf
	Arsonist
###			C	Menace 2 Society
When neutralized, witnesses Friendly
###			C	Menace 3 Society
When neutralized, witnesses Loyal
###			C	Menace 4 Society
When neutralized, witnesses Aligned
###			C	Menace -1 Society
When neutralized, witnesses Annoyed
###			C	Menace -2 Society
When neutralized, witnesses Hostile
###			C	Always Gib / Gibbous

###			C	Fearsome
Everyone terrified, Killer Robot
###			C	Focus Stealer
Camera focuses on them when in range. For bosses
###			C	Infinite Ammo
New
###			C	Infinite Consumables
New
###			C	Infinite Durability
New
###			C	No Drops
Won't drop equipment, a la Warzone
Immune to Butterfingerer
###			C	Trigger Happy
Like Killer Robot
###			C	Underclass
Cops won't protect them
###			C	Concealed Carrier
Hides weapon when out of combat.
First attempt DW
###			C	Explodevice Eligible
agent.canExplosiveStimulate
###			C	Holographic
Ghostlike, not necessarily gibbable (Use Supernatural for that)
###			C	Immobile
Can't move (for turret types)
DW
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
###			H	Statue
Remove colors
Tint white
Make stationary, Invincible, non-reactive
This should probably not be one trait, but many combined
###			C	Stinky Aura
Werewolf A-were-ness works on this character
###			C	Supernatural
Ghost Gibber works
###			C	Tattletale
Reports ALL crimes to alarm button
###			C	Translucent
Ghost visual effect
###			C	Unappetizing
Can't be bitten/cannibalized
###			C	Unchallenging
No XP for neutralization
###			C	Vision Beams (Cop Bot)
DW
###			C	Zombified
NOT Z-infected. But you might want to just Z-infect and kill, since it might be simplest.
Agent.zombified
Agent.customZombified
###			√	Berserk (Declined)
Rel General - Hostile to All
###			√	Blinker*
Complete
###			√	Brainless
Complete
###			√	Crusty
Complete
###			√	Extortable
Complete
###			√	Immovable
Complete
###			√	Indestructible
Complete
###			√	Innocent
Complete
###			√H	Guilty
agent.KillForQuest to see where those poor apartment dwellers are declared collateral damage
####			H	Cascade to Employees
SetRelationshipOriginal, under Drug Dealer
Extended this to agent.employee but I doubt that's ever actually assigned anyway.
###			√	Not Vincible
Complete
###			√	Possessed
Complete
###			√	Status Effect Immune
Complete
###			√	Z-Infected
Complete
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
###			C	Remorseful
Will do something shitty, but then regret it and go Annoyed at employer.
###			C	Remorsefuler 
As above but hostile
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
##		C	Subclass / Superclass / Parent Class (Not sure which is clearest)
One trait for each vanilla agent (32 is a lot)
Designed to make Class Solidarity worth taking for custom characters
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
###			C	Thief Network Traits (Trait Gate / Faction)
####			C	Honorable Thief
This now just means membership in the network, not necessarily Friendliness. Add it to vanilla thieves.
####			C	Tweaks to vanilla Honor Among Thieves
The relationship improvement is now scaled to the particular NPC. 
####			C	Honored Among Thieves + (Player Trait)
Those in the Network are Loyal.
####			C	Honored Among Thieves ++ (Player Trait)
Those in the Network are Aligned.
####			C	Dishonorable Thief (Player Trait)
Still pickpockets other thieves in the network
Gets access to shops in the network
CCU idea: The Secret Thief Faction
Ok not a faction, but linking Honor Among Thieves 
####			C	Ali Baba (Player Trait)
Those in the Network are mutually Annoyed
###			√	Scumbag
Complete
###			√	Slayable
Complete
###			√	Suspecter
Complete
#		C	Mutators
Setting: Force Big Quest completion - If set to yes, you will need to complete your big quest before leaving the floor, if there is one. You will not be able to exit the floor until the BQ is done, even after completing all the missions on the floor. If the quest is failed (floor only fails dont count) you will spontaneously combust and die.

Setting: Exit on Death - If set to yes, you will be forced to exit the floor when you die. On the next floor, you will be revived and you will be healed to maximum HP.

Mutator: Exit Timer - Similar to Time Limit, except that the elevator is locked until the timer reaches zero. Missions will not open the elevator.

Mutator: Exit Timer EXTREME - Similar to Time Limit EXTREME, except that the timer is LONGER, and the elevator is locked until the timer reaches zero. Missions will not open the elevator.

Mutator: Exit Timer LITE - Similar to Time Limit, except that the timer is SHORTER, and the elevator is locked until the timer reaches zero. Missions will not open the elevator. 
##		C	Disaster Mutators
Simply allows them to be mixed
##		C	Election Mutators
###			C	Vary starting election point
###			C	Vary vote value
And traits that affect vote power
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
##		√	Followers
###			√	Homesickness Disabled
Complete
###			√	Homesickness Mandatory
Complete
##		C	Gameplay
###			C	Random Start Location
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
###			C	Missions Optional
Unlock elevator
###			C	Manual Missions Only
No randos
###			C	Force Mission Type
All Neutralize, Steal, etc., but random
###			C	Hide Missions
For diegetic storytelling
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
#		CT	Objects
##			H	Ambusher
Bathtub (Need sprite)
Bush
Elevator
Gravestone
Manhole
Toilet
Tube
Well
###			C	Cannibal
###			C	Thief
##			H	Fire Status
Barbecue
Flaming Barrel
Fireplace
###			C	Lit
###			C	Unlit
##		C	Containers
###			H	Containervestigateables 
Disabling the overlap resolved all the rest of the bugs here. 
####			C	Can't remove items
Containervestigateables, once given an item, can't have it removed
####			C	Cross-Contamination
Investigateable text is displayed erroneously when selecting a containervestigateable after any investigateable. It either copies the text over or displays the old stuff.
Next attempt:
	LevelEditor.
		CloseLongDescription
		OpenLongDescription
###			C	EVS1
####			C	Keycoded
This will be my term for the Safe's lock, since it's hackable and uses a combo.
####			H	Locked
Only Desk seems eligible, but that's enough to go for it. Use stringvar1 or something as a Locked variable
Maybe generate a Desk Key item, specific to the Desk
####			C	Stashed
Stash is inaccessible (except with Object destruction) until Stash Hint is found. 
It will have almost everything in common with Key, so it should be easy research.
####			C	Stashed + Keycoded
If you keep it modular, this should be easy
####			C	Stashed + Keycoded + Locked
Why the fuck not, let's go nuts
####			C	Stashed + Locked
If you keep it modular, this should be easy
###			C	EVS2
Durability?
###			√	EVS3
Item
##			C	Object Special Actions
###				C	Alarm
###				C	Explode
###				C	Raise Dead (Chunk)
###				C	Raise Dead (Level)
###				C	Set Level Flag ABCD
This one would likely need to coexist with another OSA so...
##			C	Object Special Action Triggers
For use with Variables
###				C	On Destroy
###				C	On Investigate
When text is *closed*
###				C	On Loot
##			C	Air Conditioner
Enable "Fumigating" w/ staff in gas masks as option
GasVent.fumigationSelected
##			C	Fire Hydrant
Ability to be already shooting water according to direction
##			C	Investigateables
###				C	Limit Window Investigation to one side
Shouldn't be accessible from both sides
That code is more likely in Door than Window
###			C	French Vanilla Strings
Default strings per object type
####				C	Computer
####				C	Gravestone
Yeah Gravestone jokes are soooo funny and fresh
###			C	One-Time Read
For stuff that might not apply later, like peeking into windows
###			C	Movie Screen
Didn't work yet: https://discord.com/channels/187414758536773632/433748059172896769/1000014921305706567
Should be ready with next RL release
###			C	Custom Sprites when readable text present
Will need a visual indicator to the player. This is an extra, if RL correctly toggles interactability conditional on valid interactions.
####				C	Computer
"Unread Mail" icon on screen
###			√	Input field Display
P_LevelEditor.UpdateInterface
###			√	Input field Edit
P_LevelEditor.PressedLoadExtraVarStringList
###			√	Setup object
P_BasicObject.Spawn
###			√	Interaction
Readables.Setup
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
##				H	Suicide doesn't gib
###				H	Fix
Hook Regeneration
##			H	Relationships not Loaded
###				H	Fix
Hook Regeneration
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