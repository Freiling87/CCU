using BepInEx.Logging;
using CCU.Localization;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Reflection;

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
			// vObject.Door,		// Has Extravar field already
			vObject.Gravestone,
			// vObject.MovieScreen, // Didn't work yet, see notes
			vObject.Shelf,
			// vObject.Sign,		// No need, and interacts poorly
			// vObject.Table,
			// vObject.TableBig,
			vObject.Podium
		};

		public static string MagicObjectName(string originalName)
		{
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
						FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
						List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
						interactions.RemoveAll(i => i.ButtonName is VButtonText.ReadEmail);
					}

					h.AddImplicitButton(CButtonText.Investigate, m =>
					{
						m.Object.ShowBigImage(m.Object.extraVarString, "", null);
					});
				}
			});
		}
	}
}