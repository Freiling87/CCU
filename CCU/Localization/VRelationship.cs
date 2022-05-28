using System.Collections.Generic;

namespace CCU.Localization
{
    public class VRelationship
    {
        public const string
            Aligned = "Aligned",
            Annoyed = "Annoyed",
            Friendly = "Friendly",
            Hostile = "Hateful",
            Loyal = "Loyal",
            Neutral = "Neutral",
            Submissive = "Submissive",

            NoMoreSemiColons = "";

        /// <summary>
        /// Excludes Submissive (Unlikely to use ordinal logic)
        /// </summary>
        public static List<string> OrderedRelationships = new List<string>()
        {
            Hostile,    //  0
            Annoyed,    //  1
            Neutral,    //  2
            Friendly,   //  3
            Loyal,      //  4
            Aligned     //  5
        };
    }

}
