using BepInEx.Logging;
using CCU.Localization;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Reflection;

namespace CCU.Systems.Investigateables
{
    class Investigateables
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

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
			vObject.Shelf,
			vObject.Window,
		};

		public static string MagicObjectName(string originalName)
		{
			if (InvestigateableObjects.Contains(originalName))
				return vObject.Sign;

			return originalName;
		}

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.Investigate, t, new CustomNameInfo(CButtonText.Investigate));

			RogueInteractions.CreateProvider(h =>
			{
				if (InvestigateableObjects.Contains(h.Object.objectName) && h.Object.extraVarString != "")
				{
                    if (h.Object is Computer computer)
						h.RemoveButton(VButtonText.ReadEmail);

					h.AddImplicitButton(CButtonText.Investigate, m =>
						{
							m.Object.ShowBigImage(m.Object.extraVarString, "", null);
						});
				}
			});
		}
	}
}