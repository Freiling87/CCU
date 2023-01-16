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
			FireBomb = "FireBomb",
			Huge = "Huge",
			MindControl = "MindControl",
			NoiseOnly = "Knocker",
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
