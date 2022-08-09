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
	}

	public class P_ObjectReal_Hook : HookBase<ObjectReal>
	{
		protected override void Initialize() { }

		public InvItem stashHint = null;
		public Agent stashHintHolder = null;
		public bool stashDiscovered = false;
	}
}