using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CCU.Systems.Mutator_Configurator
{
	/// <summary>
	/// Don't look directly at it
	/// </summary>
	internal static class StringMonster
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		/// <summary>		
		/// Input:
		///		<code>[CCU]LevelGate::Type=Entry;Label=A,B;Switches=Agent,Object;Logic=AND;;</code>
		/// </summary>
		/// <param name="mutatorName"></param>
		/// <returns><code>"[CCU]LevelGate::","Type=Entry","Label=A,B","Switches=Agent,Object","Logic=AND"</code></returns>
		internal static List<string> GetElements(string mutatorName)
		{
			//	Gross!
			List<string> output = mutatorName.Replace("::", "::;").Split(';').Select(e => e = e.Replace(";", "")).ToList();
			output.RemoveAll(s => s.Length < 3);
			return output;
		}

		/// <summary>
		/// Input: 
		///		"Label=A,B;"
		/// </summary>
		/// <param name="element"></param>
		/// <returns><code>"A","B"</code></returns>
		internal static List<string> GetElementSelections(string element)
		{
			List<string> output = new List<string>(); 
			int elementStart = element.IndexOf("=");
			element = element.Substring(elementStart + 1).Replace(";", "");
			output = element.Split(',').Select(s => s.Trim()).ToList();
			return output;
		}

		/// <summary>		
		/// Input:
		///		<code>[CCU]LevelGate::Type=Entry;Label=A,B;Switches=Agent,Object;Logic=AND;;</code>
		/// </summary>
		/// <param name="mutatorName"></param>
		/// <returns><code>"Level Gate"</code></returns>
		internal static string GetDisplayHeader(string mutatorName)
		{
			int startIndex = 5;
			int endIndex = mutatorName.IndexOf("::");

			if (startIndex != -1 && endIndex != -1)
			{
				string header = mutatorName.Substring(startIndex, endIndex - startIndex).Trim();
				return header;
			}

			return string.Empty;
		}

		/// <summary>
		/// Input:
		/// <code>[CCU]LevelGate::Type=Entry;Label=A,B;Switches=Agent,Object;Logic=AND;;</code>
		/// </summary>
		/// <param name="mutatorName"></param>
		/// <returns><code>Level Gate Custom Mutator</code></returns>
		internal static string GetDisplayName(string mutatorName)
		{
			string regexHeader;

			Match match = Regex.Match(mutatorName, @"\[CCU\](.*?)::");
			if (match.Success && match.Groups.Count > 1)    //	Groups[1] = text captured by (.*?)
				regexHeader = match.Groups[1].Value;
			else
				throw new NotImplementedException();

			string output = Regex.Replace(regexHeader, "(\\B[A-Z])", " $1") + " Custom Mutator";
			return output;
		}

		/// <summary>
		/// </summary>
		/// <param name="elements"></param>
		/// <returns><code>[CCU]LevelGate::Type=Entry;Label=A,B;Switches=Agent,Object;Logic=AND;;</code></returns>
		internal static string GetDataName(List<string> elements)
		{
			string output = string.Join(";", elements);
			output = output.Replace("::;", "::").Replace(";;", ";"); // Hacky shit, who cares
			output += ";";
			return output;
		}

		internal static bool IsDataName(string arg)
		{
			logger.LogDebug("IsDataName: " + arg);
			if (string.IsNullOrWhiteSpace(arg)
				|| !arg.StartsWith(Core.CCUBlockTag))
				return false;

			logger.LogDebug("\tIsDataName A");

			List<string> elements = GetElements(arg);
			logger.LogDebug("\tIsDataName C");

			bool hasValidEntries = false;
			foreach (string element in elements)
			{
				logger.LogDebug("\t\tIsDataName C1: " + element);
				string[] keyValue = element.Split('=');

				if (keyValue.Length != 2)
					return false;

				string[] selections = keyValue[1].Split(',');
				if (selections.Length < 1)
					return false;

				logger.LogDebug("\t\tIsDataName C2");
				hasValidEntries = true;
			}

			logger.LogDebug("\tIsDataName D");
			return hasValidEntries && arg.EndsWith(";");
		}
	}
}