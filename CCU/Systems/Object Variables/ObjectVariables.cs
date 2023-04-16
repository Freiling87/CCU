using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Systems.Object_Variables
{
    class ObjectVariables
	{
		public enum SlotType
        {
			AttackMode,
			Container,
			Durability,
			ExplosionType,
			Investigation,
			None
        }

		public static Dictionary<string, SlotType[]> ObjectVariableSlots = new Dictionary<string, SlotType[]>()
		{
			{ vObject.Door, new SlotType[] { SlotType.Investigation, SlotType.None, SlotType.None } }
		};

		public static List<string> CustomObjects = new List<string>()
		{
			"CustomFloorDecal",
		};

		public static void AddCustomListEntries(List<string> vanilla) =>
			vanilla.AddRange(CustomObjects);
	}
}