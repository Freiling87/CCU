using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCU
{
	public static class BoolTools
	{
		public static bool CheckValue(List<bool> values, string operationName)
		{
			MethodInfo booleanMethod = AccessTools.Method(typeof(BoolTools), operationName);
			return (bool)booleanMethod.Invoke(null, new object[] { values });
		}

		public static bool AND(List<bool> values) =>
			values.All(v => v);
		public static bool NAND(List<bool> values) =>
			!AND(values);
		public static bool NOR(List<bool> values) =>
			!OR(values);
		public static bool OR(List<bool> values) =>
			values.Any(v => v);
		public static bool XNOR(List<bool> values) =>
			!XOR(values);
		public static bool XOR(List<bool> values) =>
			values.Where(v => v).Count() == 1;

		public static readonly Dictionary<string, string> logicGates = new Dictionary<string, string>()
		{
			{ "AND", "Equates to TRUE if all values are True."},
			{ "NAND", "Equates to TRUE if at least one value is False." },
			{ "NOR", "Equates to TRUE if all values are False." },
			{ "OR", "Equates to TRUE if at least one value is True." },
			{ "XNOR", "Equates to TRUE if all values match." },
			{ "XOR", "Equates to TRUE if only one value is True." },
		};
	}
}