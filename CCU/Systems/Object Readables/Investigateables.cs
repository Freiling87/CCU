using BepInEx.Logging;
using CCU.Localization;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Systems.Investigateables
{
    public static class Investigateables
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static string ExtraVarStringPrefix = "investigateable-message:::";

		public static List<string> InvestigateableObjects = new List<string>()
		{
			vObject.Altar,
			vObject.ArcadeGame,
			vObject.Boulder,
			vObject.Computer,
			// vObject.Counter,		// Really needs a sprite change
			vObject.Door,
			vObject.Gravestone,
			vObject.Jukebox,
			// vObject.MovieScreen, // Didn't work yet, see notes
			vObject.Podium,
			// vObject.Shelf,		// These don't play well with Containers yet.
			vObject.Speaker,
			vObject.Television,
			vObject.Window,
		};

		public static string MagicObjectName(string originalName)
		{
			if (InvestigateableObjects.Contains(originalName))
				return vObject.Sign;

			return originalName;
		}

		public static bool IsInvestigationString(string name) =>
			name?.Contains(ExtraVarStringPrefix) ?? false;

		public static List<InvSlot> FilteredSlots(InvDatabase invDatabase) =>
			invDatabase.agent.mainGUI.invInterface.Slots.Where(islot => !IsInvestigationString(islot.itemNameText.text)).ToList();

		public static InvDatabase FilteredInvDatabase(InvDatabase invDatabase)
		{
			invDatabase.InvItemList = FilteredInvItemList(invDatabase.InvItemList);
			return invDatabase;
		}

		public static List<InvItem> FilteredInvItemList(List<InvItem> invItemList) =>
			invItemList.Where(ii => !(ii.invItemName is null) && !IsInvestigationString(ii.invItemName)).ToList();

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.Investigate, t, new CustomNameInfo(CButtonText.Investigate));

			RogueInteractions.CreateProvider(h =>
			{
				if (InvestigateableObjects.Contains(h.Object.objectName) && h.Object.extraVarString != "")
				{
                    if (h.Object is Computer computer && !(h.Object.extraVarString is null) && IsInvestigationString(h.Object.extraVarString))
						h.RemoveButton(VButtonText.ReadEmail);

					h.AddImplicitButton(CButtonText.Investigate, m =>
					{
						m.Object.ShowBigImage(m.Object.extraVarString.Remove(0, ExtraVarStringPrefix.Length + 2), "", null);
					});
				}
			});
		}
	}
}