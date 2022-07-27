using RogueLibsCore;
using System.Linq;

namespace CCU.Traits.Gib_Type
{
    public abstract class T_GibType : T_CCU
    {
        public T_GibType() : base() { }

        public abstract string audioClipName { get; }
        public abstract DecalSpriteName gibDecal { get; }
        public abstract int gibQuantity { get; }
        public abstract int gibSpriteIteratorLimit { get; }
        public abstract GibSpriteNameStem gibType { get; }
        public abstract string particleEffect { get; }

        // These names need to match Gib sprite names
        public enum DecalSpriteName
        {
            BloodExplosion,
            BloodExplosionGhost, // This one might break if certain things are changed. See vanilla SpawnerMain.SpawnFloorDecal for details.
            ExplosionScorchMark,
            SlimePuddle,

            None
        }
        public enum GibSpriteNameStem
        {
            GibletGhost,
            GibletGlass,
            GibletIce,
            GibletNone,
            GibletNormal,
            GibletRobot,

            // Wall wreckage sprites not loading, alternatives on right.
            WallCaveWreckage,   FlamingBarrelWreckage,
            WallGlassWreckage,  WindowWreckage,
            WallHedgeWreckage,  BushWreckage, 
        }

        public static GibSpriteNameStem GetGibType(Agent agent) =>
            agent.GetTraits<T_GibType>().FirstOrDefault()?.gibType ?? 
                GibSpriteNameStem.GibletNormal;
    }
}