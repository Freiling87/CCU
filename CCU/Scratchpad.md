#			How to read this
If you wandered in here out of curiosity, this is my working notes file, and the place to find upcoming features. It's a markdown file, but best viewed in raw form with the Markdown Editor VS extension since a lot of its organization has to do with text characters' alignment.

Listed in order of Parent tier summary symbol priority:
	C, T = Code this, Test this
	H = Hold, usually pending resolution of a separate or grouped issue
	√ = Fully implemented feature or group of features
#			Scope
##		C	Before release
Notify Guoxin of finalized version so he can translate
##		C	v. 1.1.1 Changelog
- Compatibility
  - Verified compatibility with SOR v98 
- Bugfixes & Tweaks
  - Character Select no longer freezes when clicking an empty slot.
  - Vendor Cart "Operating" sound effect now plays for the correct duration.
  - Fixed Investigateable text showing hidden string ("investigateable-message:::").
  - Shops with Shoddy/Shiddy Goods no longer set Tasers to -1 quantity.
  - Ammo-related traits now give free ammo to NPCs with items added in the editor, rather than just items added through the Loadout system.
  - Indestructible now compatible with vanilla (Meat) Gib Type.
  - Permanent Hires no longer occupy a hire slot after death.
  - Tubes are now only valid containers for Hidden Bombs if they are within a Computer or Power Box's shutdown range.
  - Melee Maniac + now improves melee attack speed by 48.5% instead of 50%. This prevents the animation from clipping through targets.
- Feature additions
  - Items
    - Rubber Bullets Mod: Knocks out targets at 10% HP, kills at -10%. Pacifists can use Rubber Bullet guns. Not balanced!
  - Designer Traits
    - Appearance
      - Dynamic Player Appearance: Allows appearance variation when using the class as a player. Currently, appearance is only rerolled on starting a new run, but I plan to add a little more control over this in the future.
      - Fleshy Follicles: Matches hair color to skin color.
    - Behavior
      - Accident-Prone: Extended behavior to walk into Killer Plants & Laser Emitters
    - Combat
      - Toughness traits: Modify NPC willingness to fight tough odds.
      - Mag Dumper: Agent uses rapid fire for longer.
      - Melee skill traits: Modify frequency of attacks in combat.
      - Gun skill traits: Modify frequency of attacks in combat.
    - Passive
      - Crusty: Will now use Alarm Button when fleeing from combat.
      - Indomitable: Immune to mind control
    - Senses
      - Keen Ears traits: Various traits that determine how sensitive the NPC is to sound, and how they react to it.
  - Player Traits
    - Gun Nut: Agent automatically applies a mod to all eligible guns.
      - Accuracy Modder
      - Ammo Stocker
      - Rate of Fire Modder
      - Rubber Bulleteer
      - Silencerist
    - Knockback Peon: Reduces knockback, making followup attacks easier.
##		P	Bugs
Except crickets. Crickets are fine.
###				C	Syringe AV
IIRC someone said this stopped working, double-check it
###				C	Zero Money
	Maxior - The 0 money glitch is still here
	Money put in the custom containers like shelves will still only give 0 money
	Except garbage cans
###				C	Redo Pre-Build events 
Pull RL FROM plugins rather than copying TO it.
###				C	Big Quest Mandatory
	CL - Big Quest Mandatory doesn't work with Werewolf, either form. 
	NOTE: game did NOT do the red text to warn bq incomplete, which is a clue. He verified that this works on any floor.
###				C	Language
Says "I can't speak English" but the other NPC's language is Goryllian (tried interact)
###				C	Random Teleport gives Yellow Name
New
###				C	ExtraVarString copying
It's back! Yay. It's back.
###				C	Random Teleport
	CL - Spawning idle
It's just the legacy entry I think. 
###				C	Ammo Cap
CL - Having a gun already when you pick one of the traits still doesn't update the max ammo
###				T	Fearless
https://discord.com/channels/187414758536773632/1003391847902740561/1087385752754716804
Added prefixes to AssessBattle & AssessFlee
Pending confirmation from Maxior
##		P	Features
###			C	Vanillize Item Qty on load
Editor-added items give full ammo, should be semi-random and scaled to NPC level as Loadout items are
###			C	Hide Appearance Traits Button
Hides added ones from char sheet to save space
Also see about a scrollbar?
###			C	CharacterSelect.CharacterLoaded
This is a bool array that might be easier than checking for nulls on charsavedata. 
Slots 32-48 are custom characters, and it's true
##		H√	Bug Archive
###				H	Scene Setters Activate Chunk
Scene setters may cause events that the designer wished the player to witness to play out too early for them to see. Normally the trigger for activating the chunk is by player proximity.
Unfortunately, I think this fix will be a bit too complex for this project.
###				H	Hidden Bomb Spawned in Vent off map
https://discord.com/channels/187414758536773632/187414758536773632/1093294336109727845
You might be able to replace the != Wastebasket string with a special validity detector.
So far I've been unable to replicate this bug. Took place on a standard Park map that did not lack other containers for the bomb.
###				H	Equipment noise spam
Still occuring, even with vanillas.
For now, just disabling the noise if they switch to Fist.
###				H	Dismissed Perm Hire prevents Temp Hire
Can't temp hire after dismissing perm hire. 
This is MINOR, I don't care.
###				H	CCU Trait section in Load Character screen
Shows outdated traits when choosing
Don't really care yet.
###				H	Johnny Stabbs
Hold - Unable to replicate

Has Fac Blahd Aligned (replaced by Legacy)

On attempt Load from stored chars list, get error and no load:

	[Debug  :CCU_Legacy] Caught Legacy trait: Faction_Blahd_Aligned
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	MenuGUI.TextChangedCharacterDescription () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	UnityEngine.Events.InvokableCall.Invoke () (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.Events.UnityEvent`1[T0].Invoke (T0 arg0) (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.UI.InputField.SendOnValueChanged () (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.UI.InputField.SetText (System.String value, System.Boolean sendCallback) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.UI.InputField.set_text (System.String value) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	CharacterCreation.LoadCharacter2 (System.String characterName, System.Boolean secondTry, System.Boolean foundFile, System.Object mySaveObject) (at <3d326b8e9c744e5faf3a706691682c89>:0) 
	...
###				H	Clone changing appearance
Shelving this because I simply don't care enough to fix it yet. This is super-niche.
Buddy Cop Loneliness killer
The one that showed up on level 2 was identical to me
The one that came from level 1 rerolled appearance
###				H	Unidentified Eyes Strings error message
Between Accessory & Body Type, current logs are uninterrupted
Shelved - no apparent effect for this error.
###				H	Unidentified Loadout error message
Consistently, the Crepe Heavy has this error.
	[Info   : Unity Log] SETUPMORE4_7
	[Debug  :CCU_LoadoutTools] Custom Loadout: Custom(Crepe Heavy)
	[Error  : Unity Log] Couldn't do ChooseWeapon etc. for agent Custom (1132) (Agent)

This error is from LoadLevel.SetupMore.
But since there doesn't appear to be any actual repercussion on gameplay, I'm shelving it until it is an issue.
###				H	Upper Crusty Busybody 
Polices pickup and destruction of 0-Owner ID items and objects, and no others.
After reloading, was unable to replicate this.
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
###				H	$0 in container
https://discord.com/channels/187414758536773632/1003391847902740561/1007975536607383574
Maxior - Shelf w/ $0 as container, but not Trash Can
So far, unable to replicate
###				√	NPC Free Ammo
Complete. Tested items added via Loadout as well as via editor.
###				√	Partisan Concealed Carry
Winnie - https://discord.com/channels/187414758536773632/646853913273696257/1117896939837587517
Issue was Loadout Money without a Loader trait.
###				√	Error on select empty slot
####				C	Issue
On selecting empty character slot:
	[Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
	Stack trace:
	AgentHitbox.SetupBodyStrings () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CCU.Traits.App.AppearanceTools.RollEyeType (AgentHitbox agentHitbox) (at <6991a84d0f8f42f4bcade340f697d8d1>:0)
	CCU.Traits.App.AppearanceTools.SetupAppearance (AgentHitbox agentHitbox) (at <6991a84d0f8f42f4bcade340f697d8d1>:0)
	AgentHitbox.SetupFeatures () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.ShowSlotAgent () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.OpenCharacterCreation (System.Boolean resetting) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	CharacterCreation.OpenCharacterCreation () (at <3d326b8e9c744e5faf3a706691682c89>:0)
	MainGUI.ShowCharacterCreation (PlayfieldObject otherObject, Agent myAgent, System.Int32 slotNumber) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	InvSlot.ClickInvBox (UnityEngine.EventSystems.PointerEventData data, System.Boolean rightClicked) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	InvSlot.OnPointerDown (UnityEngine.EventSystems.PointerEventData data) (at <3d326b8e9c744e5faf3a706691682c89>:0)
	UnityEngine.EventSystems.ExecuteEvents.Execute (UnityEngine.EventSystems.IPointerDownHandler handler, UnityEngine.EventSystems.BaseEventData eventData) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) (at <d5bb9c19c2a7429db6c6658c41074b11>:0)
	UnityEngine.EventSystems.ExecuteEvents:ExecuteHierarchy(GameObject, BaseEventData, EventFunction`1)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMousePress(MouseButtonEventData)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMouseEvent(Int32, Int32)
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:ProcessMouseEvents()
	Rewired.Integration.UnityUI.RewiredStandaloneInputModule:Process()
	UnityEngine.EventSystems.EventSystem:Update()
####				C	Fix
Was rolling appearance without checking for CustomCharacterData. Added early return.
##		C	Mutators
###			C	Progression
####			C	Big Quest Mandatory
#####				C	Elevator Check
New
#####				C	Floor
New
#####				C	Game
New
#####				H	Chronomantic Dilation
Speeds up on suicide, but doesn't matter does it?
#####				√	Level (Instant)
Doctor, Firefighter
####		C	Custom Trait List Config
When you click it, it brings you to the Trait Config menu, where you can write, save or load presets. The chosen Trait Config applies only to that level. 
Now, does this create a new and reusable mutator? e.g. "Force Trait Config: [TraitConfigName]". Or should the user be forced to re-choose the trait config each time, so as not to clutter the trait menu?
##		H	CCU v2.0.0 Slate
Pending release of SOR2
###			C	Creator tools
####			C	Live character editor
Add or remove traits, edit their pockets, etc.
Could use code from level menus, Sandbox item generator, etc.
###			P	Upgrade to highest C# version
No better time to do this
###			C	Character Creator UI Improvements
####			C	Randomize Appearance for Appearance system
See CharacterCreation.RandomizeAppearance
Not 100% about when to trigger it generally, on Load/Save/Menu Load?
###			C	Loadout System Live Display
Show tables of item odds, separated by slot
###			C	Logging Tools
####			C	Method Identifier
Print: Method name, all arg values, name of object calling (exceptions for static, etc.)
####			C	Line ID Printer
Automatically increments, decrements, resets a couple of logging variables.
E.g., where you might use 
	logger.logDebug("B013");
Use
	logger.logCustom(0, 1);

logger.LogCustom(int branchIncrement, int lineIncrement)
	branch = A-Z
	line = 0-99
	for both args:
		null = reset to 0 or A
		int = increment by value
	Track these statically per-method, or in wider scope if needed.
###			C	Topic-based refactor
Knew it was unwise to copy the game's class structure. Move all methods into classes that revolve around a particular system, e.g. pull all the AmmoCap stuff into a single AmmoCap class. You are more likely to collide patches, but it will be easier to work on as long as you're diligent about researching those first.
###			C	Rename most methods
Generally you don't use descriptive names, and it's time to start. Maybe even split them up with Priority Attributes to control their order. You can use the Harmony Attribute arguments to know which method is targeted.
###			C	Duplicate Detection Pre-build
ChatGPT conversation:
> Can you write a method that will iterate through all my patches and identify if there are multiple that target the same method, who lack the Priority attribute? I'd like this to run as a pre-build event and output to the VS console. Then, I can add Priority to "acknowledge" the overlap and clear out the warning messages.
####				Response & Code suggestion
	To achieve this, you can create a custom pre-build event using a C# script that will analyze your code for multiple patches targeting the same method without the HarmonyPriority attribute. The script will use Roslyn, the .NET Compiler Platform, to analyze the C# source code.

	First, install the following NuGet packages in your C# project:

	Microsoft.CodeAnalysis.CSharp
	Microsoft.CodeAnalysis.CSharp.Scripting
	Next, create a script, for example, "PreBuildCheck.cs", with the following code:

		csharp
		Copy code
		using System;
		using System.Collections.Generic;
		using System.IO;
		using System.Linq;
		using System.Reflection;
		using System.Threading.Tasks;
		using Microsoft.CodeAnalysis;
		using Microsoft.CodeAnalysis.CSharp;
		using Microsoft.CodeAnalysis.CSharp.Syntax;

		namespace PreBuild
		{
			class Program
			{
				static async Task Main(string[] args)
				{
					var sourceFiles = Directory.GetFiles("path/to/your/source/folder", "*.cs", SearchOption.AllDirectories);

					var syntaxTrees = new List<SyntaxTree>();
					foreach (var sourceFile in sourceFiles)
					{
						var sourceCode = await File.ReadAllTextAsync(sourceFile);
						syntaxTrees.Add(CSharpSyntaxTree.ParseText(sourceCode));
					}

					var references = new List<MetadataReference>
					{
						MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
						MetadataReference.CreateFromFile(typeof(HarmonyLib.Harmony).Assembly.Location),
					};

					var compilation = CSharpCompilation.Create("PreBuildCheck", syntaxTrees, references);
					var semanticModel = compilation.GetSemanticModel(syntaxTrees.First());

					var methodsWithPatches = new Dictionary<IMethodSymbol, List<IMethodSymbol>>();
					foreach (var tree in syntaxTrees)
					{
						var rootNode = await tree.GetRootAsync();
						var patchMethods = rootNode.DescendantNodes()
							.OfType<MethodDeclarationSyntax>()
							.Where(m => m.AttributeLists.Any(a => a.Attributes.Any(attr => attr.Name.ToString().EndsWith("HarmonyPatch"))))
							.ToList();

						foreach (var patchMethod in patchMethods)
						{
							var methodSymbol = semanticModel.GetDeclaredSymbol(patchMethod);
							var patchAttribute = methodSymbol.GetAttributes()
								.FirstOrDefault(a => a.AttributeClass.Name.EndsWith("HarmonyPatch"));

							if (patchAttribute != null)
							{
								var targetType = (ITypeSymbol)patchAttribute.ConstructorArguments[0].Value;
								var targetMethodName = (string)patchAttribute.ConstructorArguments[1].Value;
								var targetMethod = targetType.GetMembers(targetMethodName)
									.OfType<IMethodSymbol>().FirstOrDefault();

								if (targetMethod != null)
								{
									if (!methodsWithPatches.ContainsKey(targetMethod))
									{
										methodsWithPatches[targetMethod] = new List<IMethodSymbol>();
									}
									methodsWithPatches[targetMethod].Add(methodSymbol);
								}
							}
						}
					}

					foreach (var pair in methodsWithPatches)
					{
						var targetMethod = pair.Key;
						var patches = pair.Value;

						if (patches.Count > 1)
						{
							var patchesWithoutPriority = patches.Where(p => !p.GetAttributes().Any(a => a.AttributeClass.Name.EndsWith("HarmonyPriority"))).ToList();
							if (patchesWithoutPriority.Any())
							{
								Console.WriteLine($"Multiple patches without priority found for {targetMethod}:");
								foreach (var patch in patchesWithoutPriority)
								{
									Console.WriteLine($"- {patch.ContainingType}.{patch.Name}");
								}
							}
						}
					}
				}
			}
		}
		Replace "path/to/your/source/folder" with the path to your source code folder.
####				How to implement it
Use the Priority Attribute:
	- To set the order of patches. 
	- As an implicit note to yourself that this method has received at least a baseline of attention to avoid errors.
###			C	Full sweep of legacy support
This depends on how compatible SOR1 data will be for SOR2. I don't think it will be, at all. So this might be a good chance to reform the names and organization of the content in ways that would normally disrupt legacy functions in SOR1.