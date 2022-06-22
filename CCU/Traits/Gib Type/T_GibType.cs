using RogueLibsCore;
using System.Linq;

namespace CCU.Traits.Gib_Type
{
    public abstract class T_GibType : T_CCU
    {
        public T_GibType() : base() { }

        public abstract int GibType { get; }

        public static int GetGibType(Agent agent) =>
            agent.GetTraits<T_GibType>().FirstOrDefault()?.GibType ?? 
            0;
    }
}