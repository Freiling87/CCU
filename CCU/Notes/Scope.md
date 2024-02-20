##			Feature Scope
##			C	Attempt
Superclass
Special Spawns (arcade style?)
Supernatural trait splitup group
###			P	Pre-release checks
####			BunnyLibs
- Ensure that ALL your mods work with it before releasing any. 
- If library needs a re-release, check other mods for back-compatibility and re-release those if necessary.
####			Quality Review
Review all implemented features and fixes:
- Does this need to be gated for Home Base?
- Are both NPCs and PCs addressed?
- Have you confirmed that Hooks are used as minimally as possible?
####			Project modes
- Disable Developer mode
- Make Player & Designer editions
  - Maybe this can be automated in build event?
####			Designer Traits
Notes here
####			Player Traits
Upgrades: IsAvailable = false
	Keeps from showing in levelup
###			C	Permanent Status Effects
The people clamor for them.
These are designer though, don't make player traits so RHR is still intact.
###			C	CCU Character Creation Page
Might not be impossible. See CharacterCreation.Awake.
Try COPYING something like TraitsMenu in CharacterCreation.Awake and see if you can repurpose it.
Just search Trait in that method, and do everything he does there.
###			C	Collapsible Menus at last?
https://sugarbarrel.github.io/RogueLibs/docs/dev/unlocks/configuring-unlocks
See bottom examples, "Categories"
###			T	Test out Chunk Key/ Combo
Check for:
	Duplicate key allocation
	No keys allocated
	Normal allocation taking place

###			C	Check out PlayerControl.Update
Has some debug tools that can be the basis for a bit of interesting content
###			C	OnAdded/OnRemoved Overrides
If you override these in an abstract class, you don't need to waste the space on the subclasses.