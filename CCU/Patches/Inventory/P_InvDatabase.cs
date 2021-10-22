using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using CCU.Traits;
using CCU.Traits.AI.Vendor;
using System.Reflection;
using Random = UnityEngine.Random;
using RogueLibsCore;
using CCU.Traits.Loadout;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(declaringType:typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		// TODO: AddItemReal is private and used in AddRandItem as well as fillAgent

		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.AddRandItem), argumentTypes: new[] { typeof(string) })]
		public static bool AddRandItem_Prefix(string itemNum, InvDatabase __instance, ref InvItem __result)
		{
			if (__instance.agent is null)
				return true;

			Core.LogMethodCall();

			if (TraitManager.HasTraitFromList(__instance.agent, TraitManager.VendorTypeTraits))
			{
				TraitInfo vendorTrait = TraitInfo.Get(TraitManager.GetOnlyTraitFromList(__instance.agent, TraitManager.VendorTypeTraits));
				string rName = vendorTrait.Name;

				if (__instance.CompareTag("SpecialInvDatabase") && !(rName is null))
				{
					logger.LogDebug("\trName: " + rName);
					string text;

					int num = 0;
					bool foundItem = false;

					do
					{
						try
						{
							text = __instance.rnd.RandomSelect(rName, "Items");
							text = __instance.SwapWeaponTypes(text);

							if (text != "")
								foundItem = true;
						}
						catch
						{
							logger.LogDebug("\tCatch");

							text = "Empty";
						}

						logger.LogDebug("\tText: " + text);

						foreach (InvItem invItem in __instance.InvItemList)
							if (invItem.invItemName == text && !invItem.canRepeatInShop)
								foundItem = false;

						if (text == "FreeItemVoucher")
							foundItem = false;

						num++;
					}
					while (!foundItem && num < 100);

					if (num == 100)
						text = "Empty";

					if (text != "Empty" && text != "")
					{
						MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
						__result = addItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(text);

						return false;
					}

					return false;
				}
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.FillAgent))]
		public static bool FillAgent_Prefix(InvDatabase __instance)
		{
			/* This is based on the vanilla, meant as a "first pass" to allocate important objects before other Agents get a chance.
			 * Omits everything past line 168 - that portion will take place during the normal method run after this patch, and the methods it calls will be patched rather than this one 
			 * TODO: AddRandWeapon, AddRandItem, AddStartingHeadpiece
			 * 
			 * Contains:
			 * Loadout_ChunkMayorBadge
			 * Loadout_Key
			 * Loadout_SafeCombo
			 */

			if (GC.levelType == "HomeBase")
				return false;
			
			Random.InitState(GC.loadLevel.randomSeedNum + GC.sessionDataBig.curLevelEndless + __instance.agent.agentID);
			int itemsAdded = 0;

			#region Loadout_Key, Loadout_SafeCombo
			if (__instance.agent.ownerID > 0 && __instance.agent.prisoner == 0 && (!GC.loadComplete || (GC.streamingWorld && !GC.streamingWorldController.StreamingLoaded(__instance.agent.startingChunk, __instance.agent.startingChunkReal))))
			{
				bool flag = false;
				bool flag2 = false;
			
				for (int i = 0; i < GC.objectRealList.Count; i++)
				{
					ObjectReal objectReal = GC.objectRealList[i];
				
					if ((GC.levelShape == 0 && (objectReal.owner == __instance.agent.ownerID || __instance.agent.ownerID == 99) && objectReal.startingChunk == __instance.agent.startingChunk) || 
						(GC.levelShape == 2 && objectReal.startingSector == __instance.agent.startingSector))
					{
						#region Loadout_Key
						if (__instance.agent.HasTrait<ChunkKey>() && GC.levelShape == 0 && objectReal.objectName == "Door" && objectReal.prisonObject > 0)
						{
							Door door = (Door)objectReal;
					
							if (!door.hasDetonator && door.extraVar != 10)
							{
								if (flag)
									door.distributedKey = __instance.agent;
								else if (door.distributedKey == null && door.locked)
								{
									InvItem invItem = __instance.AddItem("Key", 1);
									invItem.specificChunk = door.startingChunk;
									invItem.specificSector = door.startingSector;
									invItem.chunks.Add(door.startingChunk);
									invItem.sectors.Add(door.startingChunk);
									string text = door.startingChunkRealDescription;
									
									if (text == "Generic")
										text = "GuardPost";
										
									invItem.contents.Add(text);
									door.distributedKey = __instance.agent;
									itemsAdded++;
									flag = true;
									__instance.agent.oma.hasKey = true;
								}
							}
						}
						else if (__instance.agent.HasTrait<ChunkKey>() && GC.levelShape == 2 && objectReal.objectName == "Door" && objectReal.prisonObject == 0) // Most likely un-implemented StreamingWorld stuff, but leaving in just in case
						{
							Door door2 = (Door)objectReal;
						
							if (flag)
								door2.distributedKey = __instance.agent;
							else if (door2.distributedKey == null && door2.locked)
							{
								bool flag4 = false;
							
								if (__instance.agent.chunkFunctionType == "Key")
									flag4 = true;

								if (flag4)
								{
									InvItem invItem2 = __instance.AddItem("Key", 1);
									invItem2.specificChunk = door2.startingChunk;
									invItem2.specificSector = door2.startingSector;
									invItem2.chunks.Add(door2.startingChunk);
									invItem2.sectors.Add(door2.startingChunk);
									string text2 = door2.startingChunkRealDescription;
								
									if (text2 == "Generic")
										text2 = "GuardPost";
									
									invItem2.contents.Add(text2);
									door2.distributedKey = __instance.agent;
									itemsAdded++;
									flag = true;
									__instance.agent.oma.hasKey = true;
								}
							}
						}
						#endregion
						#region Loadout_SafeCombo
						else if (__instance.agent.HasTrait<ChunkSafeCombo>() && objectReal.objectName == "Safe")
						{
							Safe safe = (Safe)objectReal;
							
							if (flag2)
								safe.distributedKey = __instance.agent;
							else if (safe.distributedKey == null)
							{
								InvItem invItem3 = __instance.AddItem("SafeCombination", 1);
								invItem3.specificChunk = safe.startingChunk;
								invItem3.specificSector = safe.startingSector;
								invItem3.chunks.Add(safe.startingChunk);
								invItem3.sectors.Add(safe.startingChunk);
								invItem3.contents.Add(safe.startingChunkRealDescription);
								safe.distributedKey = __instance.agent;
								itemsAdded++;
								flag2 = true;
								__instance.agent.oma.hasSafeCombination = true;
							}
						}
						#endregion
					}
				}

				if (flag2 || flag)
					__instance.agent.wontFlee = true;
			}
			#endregion
			#region Loadout_ChunkMayorBadge
			if (__instance.agent.HasTrait<ChunkMayorBadge>() && __instance.agent.startingChunkRealDescription == "MayorOffice")
			{
				__instance.AddItem("MayorBadge", 1);
				__instance.agent.oma.hasMayorBadge = true;
				itemsAdded++;
			}
			#endregion

			return true;
		}

		// TODO: Replace this with AccessTools reference to private method if no modifications are needed.
		internal static InvItem AddItemReal_Custom(InvDatabase __instance, string randItem)
		{
			InvItem invItem = __instance.AddItem(randItem, 1);
			int invItemCount;

			if (invItem.invItemName == "Money")
				invItemCount = __instance.FindMoneyAmt(false);
			else if (invItem.itemType == "WeaponMelee" && __instance.CompareTag("Agent"))
			{
				if (GC.challenges.Contains("InfiniteMeleeDurability"))
					invItemCount = 100;
				else
					invItemCount = int.Parse(__instance.rnd.RandomSelect("AgentMeleeDurability", "Others"));
			}
			else if (invItem.itemType == "WeaponMelee" && __instance.CompareTag("SpecialInvDatabase") && __instance.agent != null)
				invItemCount = 200;
			else
				invItemCount = invItem.initCount;
			
			invItem.invItemCount = invItemCount;
			
			return invItem;
		}
	}
}
