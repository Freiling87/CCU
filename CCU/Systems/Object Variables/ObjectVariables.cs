using RogueLibsCore;

namespace CCU.Systems.Object_Variables
{
    class ObjectVariables
    {
	}

	public class P_ObjectReal_Hook : HookBase<ObjectReal>
	{
		protected override void Initialize() { }

		public InvItem stashHint = null;
		public Agent stashHintHolder = null;
		public bool stashDiscovered = false;
	}
}