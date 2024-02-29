##			Feature Scope
###			C	Pareto Attempts
####			C	Mugging interactions

###			P	Pre-release checks
####			C	Permanent Status Effects
Apparently there are still a few kinks to work out. I guess I can work on them in RHR and port over any fixes here.
####			BunnyLibs
- Ensure that ALL your mods work with it before releasing any.
- If library needs a re-release, check other mods for back-compatibility and re-release those if necessary.
####			Quality Review
Review all implemented features and fixes:
- Does this need to be gated for Home Base?
- Are both NPCs and PCs addressed?
- Have you confirmed that Hooks are used as minimally as possible?
- Have you checked the Player Edition interface to ensure no Designer content is visible?
####			Project modes
- Disable Developer mode
- Make Player & Designer editions
  - Maybe this can be automated in build event?
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