using BepInEx.Logging;
using CCU.Localization;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Systems.Readables
{
    class Readables
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static List<string> ReadableObjects = new List<string>()
		{
			// Commented-out entries are those that would require a sprite change
			// vObject.Altar,
			vObject.Computer,
			// vObject.Counter,
			// vObject.Door,
			vObject.Gravestone,
			vObject.MovieScreen,
			vObject.Shelf,
			vObject.Sign,
			// vObject.Table,
			// vObject.TableBig,
			vObject.Podium
		};

		public static string MagicObjectName(string originalName)
		{
			Core.LogMethodCall();
			// Need this to test inputs for LevelEditor.PressedScrollingMenuButton
			logger.LogDebug("\tInput: " + originalName);

			if (Readables.ReadableObjects.Contains(originalName))
				return "Sign";

			return originalName;
		}

		[RLSetup]
		public static void Setup()
		{
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.Investigate, t, new CustomNameInfo("Investigate"));

			RogueInteractions.CreateProvider(h =>
			{
				if (ReadableObjects.Contains(h.Object.objectName) && h.Object.extraVarString != "")
				{
					if (h.Object is Computer computer)
                    {
						// Remove vanilla "Read Email" button
					}

					h.AddImplicitButton(CButtonText.Investigate, m =>
					{
						h.Object.ShowBigImage(h.Object.extraVarString, "", null);
					});
				}
			});
		}
	}
}