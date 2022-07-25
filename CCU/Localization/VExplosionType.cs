using System.Collections.Generic;

namespace CCU.Localization
{
	public static class CExplosionType
    {
		public const string
			OilSpill = "OilSpill";

		public static List<string> Types = new List<string>
		{
			OilSpill,
		};
    }

    public static class VExplosionType
    {
		public const string
			Big = "Big",
			Dizzy = "Dizzy",
			EMP = "EMP",
			Huge = "Huge",
			FireBomb = "FireBomb",
			NoiseOnly = "Knocker",
			MindControl = "MindControl",
			Normal = "Normal",
			Ooze = "Ooze",
			PowerSap = "PowerSap",
			Ridiculous = "Ridiculous",
			Slime = "Slime",
			Stomp = "Stomp",
			Warp = "Warp",
			Water = "Water";
	}
}
